🏢 RentWise - Gestão Inteligente de Coworking
O RentWise é uma Web API robusta desenvolvida para o gerenciamento de reservas de salas em espaços de co-working. O projeto foi construído com foco em Clean Architecture e Domain-Driven Design (DDD), garantindo que as regras de negócio sejam o coração da aplicação.

🛠️ Tecnologias e Arquitetura
Framework: .NET 10 (C#).

Persistência: Entity Framework Core com SQL Server.

Arquitetura: Clean Architecture, dividida em camadas para maior testabilidade e manutenção:

Core: Entidades e interfaces.

Infrastructure: Contexto do banco de dados, Mappings e Repositories.

API: Controllers e documentação.

Documentação: Swagger/OpenAPI interativo.

💡 Diferenciais Técnicos
🔒 Domain-Driven Validation
As regras de negócio não são apenas "filtros" no Controller, mas estão protegidas dentro das Entidades:

Cálculo Automático: O valor total da reserva é calculado no domínio com base no preço por hora da sala.

Proteção de Cancelamento: Implementação de trava lógica que impede o cancelamento de reservas com menos de 2 horas de antecedência.

🛡️ Prevenção de Conflitos (Concurrency)
Desenvolvi uma lógica no Repository que verifica a disponibilidade de horários de forma assíncrona antes de confirmar qualquer reserva:

O sistema impede que uma sala seja ocupada por dois usuários no mesmo período, tratando sobreposições de horários com precisão matemática.

📊 Fluent Mapping & Database Integrity
Utilização de Fluent API para configurar o banco de dados de forma desacoplada da entidade.

Configuração de Precisão Decimal (18,2) em campos financeiros para garantir a integridade dos valores cobrados e evitar erros de arredondamento no SQL Server.

🚀 Como Executar
Clone o repositório.

Configure a ConnectionString no appsettings.json.

Execute Update-Database para criar as tabelas.

Rode o projeto e acesse o Swagger na página inicial.
