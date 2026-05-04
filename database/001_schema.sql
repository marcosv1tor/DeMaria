BEGIN;

CREATE TABLE IF NOT EXISTS clientes (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    documento VARCHAR(30) NOT NULL,
    tipo VARCHAR(10) NOT NULL,
    email VARCHAR(150),
    telefone VARCHAR(30),
    data_cadastro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT uq_clientes_documento UNIQUE (documento),
    CONSTRAINT ck_clientes_tipo CHECK (tipo IN ('Fisica', 'Juridica'))
);

CREATE TABLE IF NOT EXISTS servicos (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    valor_base NUMERIC(12,2) NOT NULL,
    percentual_imposto NUMERIC(5,2) NOT NULL DEFAULT 0,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT ck_servicos_valor_base CHECK (valor_base > 0),
    CONSTRAINT ck_servicos_percentual_imposto CHECK (percentual_imposto >= 0 AND percentual_imposto <= 100)
);

CREATE TABLE IF NOT EXISTS ordens_servico (
    id SERIAL PRIMARY KEY,
    cliente_id INTEGER NOT NULL,
    data_abertura TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_conclusao TIMESTAMP NULL,
    status VARCHAR(20) NOT NULL,
    observacao TEXT,
    valor_total NUMERIC(12,2) NOT NULL DEFAULT 0,
    versao INTEGER NOT NULL DEFAULT 1,
    CONSTRAINT fk_ordens_servico_cliente FOREIGN KEY (cliente_id) REFERENCES clientes(id),
    CONSTRAINT ck_ordens_servico_status CHECK (status IN ('Aberta', 'EmAndamento', 'Concluida', 'Cancelada')),
    CONSTRAINT ck_ordens_servico_valor_total CHECK (valor_total >= 0),
    CONSTRAINT ck_ordens_servico_versao CHECK (versao > 0),
    CONSTRAINT ck_ordens_servico_conclusao CHECK (
        (status = 'Concluida' AND data_conclusao IS NOT NULL)
        OR (status <> 'Concluida')
    )
);

CREATE TABLE IF NOT EXISTS ordem_servico_itens (
    id SERIAL PRIMARY KEY,
    ordem_servico_id INTEGER NOT NULL,
    servico_id INTEGER NOT NULL,
    quantidade NUMERIC(12,2) NOT NULL,
    valor_unitario NUMERIC(12,2) NOT NULL,
    percentual_imposto_aplicado NUMERIC(5,2) NOT NULL,
    valor_total_item NUMERIC(12,2) NOT NULL,
    CONSTRAINT fk_os_itens_ordem FOREIGN KEY (ordem_servico_id) REFERENCES ordens_servico(id) ON DELETE CASCADE,
    CONSTRAINT fk_os_itens_servico FOREIGN KEY (servico_id) REFERENCES servicos(id),
    CONSTRAINT ck_os_itens_quantidade CHECK (quantidade > 0),
    CONSTRAINT ck_os_itens_valor_unitario CHECK (valor_unitario > 0),
    CONSTRAINT ck_os_itens_percentual_imposto CHECK (percentual_imposto_aplicado >= 0 AND percentual_imposto_aplicado <= 100),
    CONSTRAINT ck_os_itens_valor_total CHECK (valor_total_item >= 0)
);

CREATE TABLE IF NOT EXISTS historico_status (
    id SERIAL PRIMARY KEY,
    ordem_servico_id INTEGER NOT NULL,
    status_anterior VARCHAR(20),
    status_novo VARCHAR(20) NOT NULL,
    data_hora TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario VARCHAR(100) NOT NULL,
    CONSTRAINT fk_historico_status_ordem FOREIGN KEY (ordem_servico_id) REFERENCES ordens_servico(id) ON DELETE CASCADE,
    CONSTRAINT ck_historico_status_anterior CHECK (status_anterior IS NULL OR status_anterior IN ('Aberta', 'EmAndamento', 'Concluida', 'Cancelada')),
    CONSTRAINT ck_historico_status_novo CHECK (status_novo IN ('Aberta', 'EmAndamento', 'Concluida', 'Cancelada'))
);

CREATE TABLE IF NOT EXISTS auditorias (
    id SERIAL PRIMARY KEY,
    entidade VARCHAR(80) NOT NULL,
    id_registro INTEGER NOT NULL,
    operacao VARCHAR(10) NOT NULL,
    data_hora TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario VARCHAR(100) NOT NULL,
    snapshot_json JSONB NOT NULL,
    CONSTRAINT ck_auditorias_operacao CHECK (operacao IN ('INSERT', 'UPDATE', 'DELETE'))
);

CREATE INDEX IF NOT EXISTS ix_clientes_documento ON clientes(documento);
CREATE INDEX IF NOT EXISTS ix_ordens_servico_data_abertura ON ordens_servico(data_abertura);
CREATE INDEX IF NOT EXISTS ix_ordens_servico_status ON ordens_servico(status);
CREATE INDEX IF NOT EXISTS ix_ordens_servico_cliente_id ON ordens_servico(cliente_id);
CREATE INDEX IF NOT EXISTS ix_os_itens_ordem_servico_id ON ordem_servico_itens(ordem_servico_id);
CREATE INDEX IF NOT EXISTS ix_historico_status_ordem_servico_id ON historico_status(ordem_servico_id);
CREATE INDEX IF NOT EXISTS ix_auditorias_entidade_registro ON auditorias(entidade, id_registro);

CREATE INDEX IF NOT EXISTS ix_clientes_ativos ON clientes(nome) WHERE ativo = TRUE;
CREATE INDEX IF NOT EXISTS ix_servicos_ativos ON servicos(nome) WHERE ativo = TRUE;

COMMIT;
