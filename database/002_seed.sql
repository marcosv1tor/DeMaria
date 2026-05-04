BEGIN;

INSERT INTO clientes (nome, documento, tipo, email, telefone)
VALUES
    ('Maria Oliveira', '12345678901', 'Fisica', 'maria.oliveira@example.com', '(11) 90000-0001'),
    ('Acme Servicos Ltda', '12345678000190', 'Juridica', 'financeiro@acme.example.com', '(11) 3000-0000')
ON CONFLICT (documento) DO NOTHING;

INSERT INTO servicos (nome, valor_base, percentual_imposto)
VALUES
    ('Manutencao preventiva', 250.00, 8.50),
    ('Instalacao tecnica', 480.00, 12.00),
    ('Suporte remoto', 120.00, 5.00);

COMMIT;
