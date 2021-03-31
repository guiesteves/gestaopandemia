using CVC19.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//https://docs.microsoft.com/ptbr/ef/core/modeling/dataseeding

namespace CVC19.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AgentePatogenico> AgentePatogenico { get; set; }
        public DbSet<Laboratorio> Laboratorio { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<TipoAgentePatogenico> TipoAgentePatogenico { get; set; }
        public DbSet<TipoVacina> TipoVacina { get; set; }
        public DbSet<Vacina> Vacina { get; set; }
        public DbSet<VarianteAgentePatogenico> VarianteAgentePatogenico { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "ADMIN", NormalizedName = "ADMIN" },
               new IdentityRole { Id = "2", Name = "GESTOR_VACINA", NormalizedName = "GESTOR_VACINA" },
               new IdentityRole { Id = "3", Name = "GESTOR_PATOGENO", NormalizedName = "GESTOR_PATOGENO" }
            );

            builder.Entity<IdentityUser>().HasData(
               new IdentityUser { 
                   Id = "1ff6e1f7-2049-434b-8e5a-93d454203652", 
                   UserName = "gestor.patogeno@gestaopandemia.com.br", 
                   Email = "gestor.patogeno@gestaopandemia.com.br", 
                   PasswordHash = "AQAAAAEAACcQAAAAEDnGJexv5zVzdMX9JOmG2bzgpcOLKfy9ra6cMqZr8JGIdxq7PxqR1lkCU+Pg0Gqecw==",
                   SecurityStamp = "KNIHZXZV42FGMK7XZDGEIX6P6QPUICGU",
                   ConcurrencyStamp = "0799389d-af42-4530-8b63-4efff19fb702",
                   NormalizedEmail = "GESTOR.PATOGENO@GESTAOPANDEMIA.COM.BR",
                   NormalizedUserName = "GESTOR.PATOGENO@GESTAOPANDEMIA.COM.BR",
                   EmailConfirmed = false,
                   LockoutEnabled = false,
                   PhoneNumberConfirmed = false,
                   AccessFailedCount = 0
               },

               new IdentityUser { 
                   Id = "3872c9f1-555b-486f-a5e5-5f0de852b677", 
                   UserName = "gestor.vacina@gestaopandemia.com.br", 
                   Email = "gestor.vacina@gestaopandemia.com.br",
                   PasswordHash = "AQAAAAEAACcQAAAAEEhruVTtYGoBjbjyyKVnIx10uuPr5Me0VYWkUwPXZynAOSpAZxlk8Umwj92FDimzgw==",
                   SecurityStamp = "Z2YZBTQXYJYYTIN2ORODRQWI3RPJN5ZS",
                   ConcurrencyStamp = "efedc7fc-f4b6-444e-be24-61aef4f24f0e",
                   NormalizedEmail = "GESTOR.VACINA@GESTAOPANDEMIA.COM.BR",
                   NormalizedUserName = "GESTOR.VACINA@GESTAOPANDEMIA.COM.BR",
                   EmailConfirmed = false,
                   LockoutEnabled = false,
                   PhoneNumberConfirmed = false,
                   AccessFailedCount = 0
               },

               new IdentityUser { 
                   Id = "95c36366-0c04-41fc-b317-f77655da019d", 
                   UserName = "adminstrador@gestaopandemia.com.br", 
                   Email = "adminstrador@gestaopandemia.com.br",
                   PasswordHash = "AQAAAAEAACcQAAAAEBhFLa833zCa3xvqBMqCpq4eJN2Ub7k91Dqdyh0Fgf2RuX3KVcw6HKFZpXCRXEoWTA==",
                   SecurityStamp = "YWCTGIE4H4U22NNV5K4NUA6EH5MQYG5B",
                   ConcurrencyStamp = "62e0cbc7-8747-4dbb-8bdd-e256c5b6d3cf",
                   NormalizedEmail = "ADMINSTRADOR@GESTAOPANDEMIA.COM.BR",
                   NormalizedUserName = "ADMINSTRADOR@GESTAOPANDEMIA.COM.BR",
                   EmailConfirmed = false,
                   LockoutEnabled = false,
                   PhoneNumberConfirmed = false,
                   AccessFailedCount = 0
               }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
              new IdentityUserRole<string> { UserId = "95c36366-0c04-41fc-b317-f77655da019d", RoleId = "1" },
              new IdentityUserRole<string> { UserId = "3872c9f1-555b-486f-a5e5-5f0de852b677", RoleId = "2" },
              new IdentityUserRole<string> { UserId = "1ff6e1f7-2049-434b-8e5a-93d454203652", RoleId = "3" }
           );

            builder.Entity<TipoAgentePatogenico>().HasData(
               new TipoAgentePatogenico { TipoAgentePatogenicoId = 1, Nome = "Bactéria" },
               new TipoAgentePatogenico { TipoAgentePatogenicoId = 2, Nome = "Fungo" },
               new TipoAgentePatogenico { TipoAgentePatogenicoId = 3, Nome = "Protozoário" },
               new TipoAgentePatogenico { TipoAgentePatogenicoId = 4, Nome = "Vírus" }
            );


            builder.Entity<TipoVacina>().HasData(
                new TipoVacina { TipoVacinaId = 1, Nome = "Patógeno Inativado", Descricao = "Usa o patógeno completo e inativo (\"Morto\"). A vacina tem todas as proteínas do vírus" },
                new TipoVacina { TipoVacinaId = 2, Nome = "Vetor Patógeno", Descricao = "Usa um outro patógeno para carregar o material genético para dentro da célula" },
                new TipoVacina { TipoVacinaId = 3, Nome = "RNA", Descricao = "Usa o material genético (RNA) do patógeno para que fornecer as instruções para que a célula produz a proteína do patógeno" },
                new TipoVacina { TipoVacinaId = 4, Nome = "Subunidade de Proteínas", Descricao = "Fornece a proteína para estimular o sistema imunológico" }
            );

            builder.Entity<Pais>().HasData(
                 new Pais { PaisId = 1, Sigla = "AF", Nome = "Afeganistão" },
                 new Pais { PaisId = 2, Sigla = "ZA", Nome = "África do Sul" },
                 new Pais { PaisId = 3, Sigla = "AX", Nome = "Åland, Ilhas" },
                 new Pais { PaisId = 4, Sigla = "AL", Nome = "Albânia" },
                 new Pais { PaisId = 5, Sigla = "DE", Nome = "Alemanha" },
                 new Pais { PaisId = 6, Sigla = "AD", Nome = "Andorra" },
                 new Pais { PaisId = 7, Sigla = "AO", Nome = "Angola" },
                 new Pais { PaisId = 8, Sigla = "AI", Nome = "Anguilla" },
                 new Pais { PaisId = 9, Sigla = "AQ", Nome = "Antártica" },
                 new Pais { PaisId = 10, Sigla = "AG", Nome = "Antigua e Barbuda" },
                 new Pais { PaisId = 11, Sigla = "AN", Nome = "Antilhas Holandesas" },
                 new Pais { PaisId = 12, Sigla = "SA", Nome = "Arábia Saudita" },
                 new Pais { PaisId = 13, Sigla = "DZ", Nome = "Argélia" },
                 new Pais { PaisId = 14, Sigla = "AR", Nome = "Argentina" },
                 new Pais { PaisId = 15, Sigla = "AM", Nome = "Arménia" },
                 new Pais { PaisId = 16, Sigla = "AW", Nome = "Aruba" },
                 new Pais { PaisId = 17, Sigla = "AU", Nome = "Austrália" },
                 new Pais { PaisId = 18, Sigla = "AT", Nome = "Áustria" },
                 new Pais { PaisId = 19, Sigla = "AZ", Nome = "Azerbeijão" },
                 new Pais { PaisId = 20, Sigla = "BS", Nome = "Bahamas" },
                 new Pais { PaisId = 21, Sigla = "BH", Nome = "Bahrain" },
                 new Pais { PaisId = 22, Sigla = "BD", Nome = "Bangladesh" },
                 new Pais { PaisId = 23, Sigla = "BB", Nome = "Barbados" },
                 new Pais { PaisId = 24, Sigla = "BE", Nome = "Bélgica" },
                 new Pais { PaisId = 25, Sigla = "BZ", Nome = "Belize" },
                 new Pais { PaisId = 26, Sigla = "BJ", Nome = "Benin" },
                 new Pais { PaisId = 27, Sigla = "BM", Nome = "Bermuda" },
                 new Pais { PaisId = 28, Sigla = "BY", Nome = "BieloRússia" },
                 new Pais { PaisId = 29, Sigla = "BO", Nome = "Bolívia" },
                 new Pais { PaisId = 30, Sigla = "BA", Nome = "BósniaHerzegovina" },
                 new Pais { PaisId = 31, Sigla = "BW", Nome = "Botswana" },
                 new Pais { PaisId = 32, Sigla = "BV", Nome = "Bouvet, Ilha" },
                 new Pais { PaisId = 33, Sigla = "BR", Nome = "Brasil" },
                 new Pais { PaisId = 34, Sigla = "BN", Nome = "Brunei" },
                 new Pais { PaisId = 35, Sigla = "BG", Nome = "Bulgária" },
                 new Pais { PaisId = 36, Sigla = "BF", Nome = "Burkina Faso" },
                 new Pais { PaisId = 37, Sigla = "BI", Nome = "Burundi" },
                 new Pais { PaisId = 38, Sigla = "BT", Nome = "Butão" },
                 new Pais { PaisId = 39, Sigla = "CV", Nome = "Cabo Verde" },
                 new Pais { PaisId = 40, Sigla = "KH", Nome = "Cambodja" },
                 new Pais { PaisId = 41, Sigla = "CM", Nome = "Camarões" },
                 new Pais { PaisId = 42, Sigla = "CA", Nome = "Canadá" },
                 new Pais { PaisId = 43, Sigla = "KY", Nome = "Cayman, Ilhas" },
                 new Pais { PaisId = 44, Sigla = "KZ", Nome = "Cazaquistão" },
                 new Pais { PaisId = 45, Sigla = "CF", Nome = "Centroafricana, República" },
                 new Pais { PaisId = 46, Sigla = "TD", Nome = "Chade" },
                 new Pais { PaisId = 47, Sigla = "CZ", Nome = "Checa, República" },
                 new Pais { PaisId = 48, Sigla = "CL", Nome = "Chile" },
                 new Pais { PaisId = 49, Sigla = "CN", Nome = "China" },
                 new Pais { PaisId = 50, Sigla = "CY", Nome = "Chipre" },
                 new Pais { PaisId = 51, Sigla = "CX", Nome = "Christmas, Ilha" },
                 new Pais { PaisId = 52, Sigla = "CC", Nome = "Cocos, Ilhas" },
                 new Pais { PaisId = 53, Sigla = "CO", Nome = "Colômbia" },
                 new Pais { PaisId = 54, Sigla = "KM", Nome = "Comores" },
                 new Pais { PaisId = 55, Sigla = "CG", Nome = "Congo, República do" },
                 new Pais { PaisId = 56, Sigla = "CD", Nome = "Congo, República Democrática do (antigo Zaire)" },
                 new Pais { PaisId = 57, Sigla = "CK", Nome = "Cook, Ilhas" },
                 new Pais { PaisId = 58, Sigla = "KR", Nome = "Coreia do Sul" },
                 new Pais { PaisId = 59, Sigla = "KP", Nome = "Coreia, República Democrática da (Coreia do Norte)" },
                 new Pais { PaisId = 60, Sigla = "CI", Nome = "Costa do Marfim" },
                 new Pais { PaisId = 61, Sigla = "CR", Nome = "Costa Rica" },
                 new Pais { PaisId = 62, Sigla = "HR", Nome = "Croácia" },
                 new Pais { PaisId = 63, Sigla = "CU", Nome = "Cuba" },
                 new Pais { PaisId = 64, Sigla = "DK", Nome = "Dinamarca" },
                 new Pais { PaisId = 65, Sigla = "DJ", Nome = "Djibouti" },
                 new Pais { PaisId = 66, Sigla = "DM", Nome = "Dominica" },
                 new Pais { PaisId = 67, Sigla = "DO", Nome = "Dominicana, República" },
                 new Pais { PaisId = 68, Sigla = "EG", Nome = "Egipto" },
                 new Pais { PaisId = 69, Sigla = "SV", Nome = "El Salvador" },
                 new Pais { PaisId = 70, Sigla = "AE", Nome = "Emiratos Árabes Unidos" },
                 new Pais { PaisId = 71, Sigla = "EC", Nome = "Equador" },
                 new Pais { PaisId = 72, Sigla = "ER", Nome = "Eritreia" },
                 new Pais { PaisId = 73, Sigla = "SK", Nome = "Eslováquia" },
                 new Pais { PaisId = 74, Sigla = "SI", Nome = "Eslovénia" },
                 new Pais { PaisId = 75, Sigla = "ES", Nome = "Espanha" },
                 new Pais { PaisId = 76, Sigla = "US", Nome = "Estados Unidos da América" },
                 new Pais { PaisId = 77, Sigla = "EE", Nome = "Estónia" },
                 new Pais { PaisId = 78, Sigla = "ET", Nome = "Etiópia" },
                 new Pais { PaisId = 79, Sigla = "FO", Nome = "Faroe, Ilhas" },
                 new Pais { PaisId = 80, Sigla = "FJ", Nome = "Fiji" },
                 new Pais { PaisId = 81, Sigla = "PH", Nome = "Filipinas" },
                 new Pais { PaisId = 82, Sigla = "FI", Nome = "Finlândia" },
                 new Pais { PaisId = 83, Sigla = "FR", Nome = "França" },
                 new Pais { PaisId = 84, Sigla = "GA", Nome = "Gabão" },
                 new Pais { PaisId = 85, Sigla = "GM", Nome = "Gâmbia" },
                 new Pais { PaisId = 86, Sigla = "GH", Nome = "Gana" },
                 new Pais { PaisId = 87, Sigla = "GE", Nome = "Geórgia" },
                 new Pais { PaisId = 88, Sigla = "GS", Nome = "Geórgia do Sul e Sandwich do Sul, Ilhas" },
                 new Pais { PaisId = 89, Sigla = "GI", Nome = "Gibraltar" },
                 new Pais { PaisId = 90, Sigla = "GR", Nome = "Grécia" },
                 new Pais { PaisId = 91, Sigla = "GD", Nome = "Grenada" },
                 new Pais { PaisId = 92, Sigla = "GL", Nome = "Gronelândia" },
                 new Pais { PaisId = 93, Sigla = "GP", Nome = "Guadeloupe" },
                 new Pais { PaisId = 94, Sigla = "GU", Nome = "Guam" },
                 new Pais { PaisId = 95, Sigla = "GT", Nome = "Guatemala" },
                 new Pais { PaisId = 96, Sigla = "GG", Nome = "Guernsey" },
                 new Pais { PaisId = 97, Sigla = "GY", Nome = "Guiana" },
                 new Pais { PaisId = 98, Sigla = "GF", Nome = "Guiana Francesa" },
                 new Pais { PaisId = 99, Sigla = "GW", Nome = "GuinéBissau" },
                 new Pais { PaisId = 100, Sigla = "GN", Nome = "GuinéConacri" },
                 new Pais { PaisId = 101, Sigla = "GQ", Nome = "Guiné Equatorial" },
                 new Pais { PaisId = 102, Sigla = "HT", Nome = "Haiti" },
                 new Pais { PaisId = 103, Sigla = "HM", Nome = "Heard e Ilhas McDonald, Ilha" },
                 new Pais { PaisId = 104, Sigla = "HN", Nome = "Honduras" },
                 new Pais { PaisId = 105, Sigla = "HK", Nome = "Hong Kong" },
                 new Pais { PaisId = 106, Sigla = "HU", Nome = "Hungria" },
                 new Pais { PaisId = 107, Sigla = "YE", Nome = "Iémen" },
                 new Pais { PaisId = 108, Sigla = "IN", Nome = "Índia" },
                 new Pais { PaisId = 109, Sigla = "ID", Nome = "Indonésia" },
                 new Pais { PaisId = 110, Sigla = "IQ", Nome = "Iraque" },
                 new Pais { PaisId = 111, Sigla = "IR", Nome = "Irão" },
                 new Pais { PaisId = 112, Sigla = "IE", Nome = "Irlanda" },
                 new Pais { PaisId = 113, Sigla = "IS", Nome = "Islândia" },
                 new Pais { PaisId = 114, Sigla = "IL", Nome = "Israel" },
                 new Pais { PaisId = 115, Sigla = "IT", Nome = "Itália" },
                 new Pais { PaisId = 116, Sigla = "JM", Nome = "Jamaica" },
                 new Pais { PaisId = 117, Sigla = "JP", Nome = "Japão" },
                 new Pais { PaisId = 118, Sigla = "JE", Nome = "Jersey" },
                 new Pais { PaisId = 119, Sigla = "JO", Nome = "Jordânia" },
                 new Pais { PaisId = 120, Sigla = "KI", Nome = "Kiribati" },
                 new Pais { PaisId = 121, Sigla = "KW", Nome = "Kuwait" },
                 new Pais { PaisId = 122, Sigla = "LA", Nome = "Laos" },
                 new Pais { PaisId = 123, Sigla = "LS", Nome = "Lesoto" },
                 new Pais { PaisId = 124, Sigla = "LV", Nome = "Letónia" },
                 new Pais { PaisId = 125, Sigla = "LB", Nome = "Líbano" },
                 new Pais { PaisId = 126, Sigla = "LR", Nome = "Libéria" },
                 new Pais { PaisId = 127, Sigla = "LY", Nome = "Líbia" },
                 new Pais { PaisId = 128, Sigla = "LI", Nome = "Liechtenstein" },
                 new Pais { PaisId = 129, Sigla = "LT", Nome = "Lituânia" },
                 new Pais { PaisId = 130, Sigla = "LU", Nome = "Luxemburgo" },
                 new Pais { PaisId = 131, Sigla = "MO", Nome = "Macau" },
                 new Pais { PaisId = 132, Sigla = "MK", Nome = "Macedónia, República da" },
                 new Pais { PaisId = 133, Sigla = "MG", Nome = "Madagáscar" },
                 new Pais { PaisId = 134, Sigla = "MY", Nome = "Malásia" },
                 new Pais { PaisId = 135, Sigla = "MW", Nome = "Malawi" },
                 new Pais { PaisId = 136, Sigla = "MV", Nome = "Maldivas" },
                 new Pais { PaisId = 137, Sigla = "ML", Nome = "Mali" },
                 new Pais { PaisId = 138, Sigla = "MT", Nome = "Malta" },
                 new Pais { PaisId = 139, Sigla = "FK", Nome = "Malvinas, Ilhas (Falkland)" },
                 new Pais { PaisId = 140, Sigla = "IM", Nome = "Man, Ilha de" },
                 new Pais { PaisId = 141, Sigla = "MP", Nome = "Marianas Setentrionais" },
                 new Pais { PaisId = 142, Sigla = "MA", Nome = "Marrocos" },
                 new Pais { PaisId = 143, Sigla = "MH", Nome = "Marshall, Ilhas" },
                 new Pais { PaisId = 144, Sigla = "MQ", Nome = "Martinica" },
                 new Pais { PaisId = 145, Sigla = "MU", Nome = "Maurícia" },
                 new Pais { PaisId = 146, Sigla = "MR", Nome = "Mauritânia" },
                 new Pais { PaisId = 147, Sigla = "YT", Nome = "Mayotte" },
                 new Pais { PaisId = 148, Sigla = "UM", Nome = "Menores Distantes dos Estados Unidos, Ilhas" },
                 new Pais { PaisId = 149, Sigla = "MX", Nome = "México" },
                 new Pais { PaisId = 150, Sigla = "MM", Nome = "Myanmar (antiga Birmânia)" },
                 new Pais { PaisId = 151, Sigla = "FM", Nome = "Micronésia, Estados Federados da" },
                 new Pais { PaisId = 152, Sigla = "MZ", Nome = "Moçambique" },
                 new Pais { PaisId = 153, Sigla = "MD", Nome = "Moldávia" },
                 new Pais { PaisId = 154, Sigla = "MC", Nome = "Mónaco" },
                 new Pais { PaisId = 155, Sigla = "MN", Nome = "Mongólia" },
                 new Pais { PaisId = 156, Sigla = "ME", Nome = "Montenegro" },
                 new Pais { PaisId = 157, Sigla = "MS", Nome = "Montserrat" },
                 new Pais { PaisId = 158, Sigla = "NA", Nome = "Namíbia" },
                 new Pais { PaisId = 159, Sigla = "NR", Nome = "Nauru" },
                 new Pais { PaisId = 160, Sigla = "NP", Nome = "Nepal" },
                 new Pais { PaisId = 161, Sigla = "NI", Nome = "Nicarágua" },
                 new Pais { PaisId = 162, Sigla = "NE", Nome = "Níger" },
                 new Pais { PaisId = 163, Sigla = "NG", Nome = "Nigéria" },
                 new Pais { PaisId = 164, Sigla = "NU", Nome = "Niue" },
                 new Pais { PaisId = 165, Sigla = "NF", Nome = "Norfolk, Ilha" },
                 new Pais { PaisId = 166, Sigla = "NO", Nome = "Noruega" },
                 new Pais { PaisId = 167, Sigla = "NC", Nome = "Nova Caledónia" },
                 new Pais { PaisId = 168, Sigla = "NZ", Nome = "Nova Zelândia (Aotearoa)" },
                 new Pais { PaisId = 169, Sigla = "OM", Nome = "Oman" },
                 new Pais { PaisId = 170, Sigla = "NL", Nome = "Países Baixos (Holanda)" },
                 new Pais { PaisId = 171, Sigla = "PW", Nome = "Palau" },
                 new Pais { PaisId = 172, Sigla = "PS", Nome = "Palestina" },
                 new Pais { PaisId = 173, Sigla = "PA", Nome = "Panamá" },
                 new Pais { PaisId = 174, Sigla = "PG", Nome = "PapuaNova Guiné" },
                 new Pais { PaisId = 175, Sigla = "PK", Nome = "Paquistão" },
                 new Pais { PaisId = 176, Sigla = "PY", Nome = "Paraguai" },
                 new Pais { PaisId = 177, Sigla = "PE", Nome = "Peru" },
                 new Pais { PaisId = 178, Sigla = "PN", Nome = "Pitcairn" },
                 new Pais { PaisId = 179, Sigla = "PF", Nome = "Polinésia Francesa" },
                 new Pais { PaisId = 180, Sigla = "PL", Nome = "Polónia" },
                 new Pais { PaisId = 181, Sigla = "PR", Nome = "Porto Rico" },
                 new Pais { PaisId = 182, Sigla = "PT", Nome = "Portugal" },
                 new Pais { PaisId = 183, Sigla = "QA", Nome = "Qatar" },
                 new Pais { PaisId = 184, Sigla = "KE", Nome = "Quénia" },
                 new Pais { PaisId = 185, Sigla = "KG", Nome = "Quirguistão" },
                 new Pais { PaisId = 186, Sigla = "GB", Nome = "Reino Unido da GrãBretanha e Irlanda do Norte" },
                 new Pais { PaisId = 187, Sigla = "RE", Nome = "Reunião" },
                 new Pais { PaisId = 188, Sigla = "RO", Nome = "Roménia" },
                 new Pais { PaisId = 189, Sigla = "RW", Nome = "Ruanda" },
                 new Pais { PaisId = 190, Sigla = "RU", Nome = "Rússia" },
                 new Pais { PaisId = 191, Sigla = "EH", Nome = "Saara Ocidental" },
                 new Pais { PaisId = 192, Sigla = "AS", Nome = "Samoa Americana" },
                 new Pais { PaisId = 193, Sigla = "WS", Nome = "Samoa (Samoa Ocidental)" },
                 new Pais { PaisId = 194, Sigla = "PM", Nome = "Saint Pierre et Miquelon" },
                 new Pais { PaisId = 195, Sigla = "SB", Nome = "Salomão, Ilhas" },
                 new Pais { PaisId = 196, Sigla = "KN", Nome = "São Cristóvão e Névis (Saint Kitts e Nevis)" },
                 new Pais { PaisId = 197, Sigla = "SM", Nome = "San Marino" },
                 new Pais { PaisId = 198, Sigla = "ST", Nome = "São Tomé e Príncipe" },
                 new Pais { PaisId = 199, Sigla = "VC", Nome = "São Vicente e Granadinas" },
                 new Pais { PaisId = 200, Sigla = "SH", Nome = "Santa Helena" },
                 new Pais { PaisId = 201, Sigla = "LC", Nome = "Santa Lúcia" },
                 new Pais { PaisId = 202, Sigla = "SN", Nome = "Senegal" },
                 new Pais { PaisId = 203, Sigla = "SL", Nome = "Serra Leoa" },
                 new Pais { PaisId = 204, Sigla = "RS", Nome = "Sérvia" },
                 new Pais { PaisId = 205, Sigla = "SC", Nome = "Seychelles" },
                 new Pais { PaisId = 206, Sigla = "SG", Nome = "Singapura" },
                 new Pais { PaisId = 207, Sigla = "SY", Nome = "Síria" },
                 new Pais { PaisId = 208, Sigla = "SO", Nome = "Somália" },
                 new Pais { PaisId = 209, Sigla = "LK", Nome = "Sri Lanka" },
                 new Pais { PaisId = 210, Sigla = "SZ", Nome = "Suazilândia" },
                 new Pais { PaisId = 211, Sigla = "SD", Nome = "Sudão" },
                 new Pais { PaisId = 212, Sigla = "SE", Nome = "Suécia" },
                 new Pais { PaisId = 213, Sigla = "CH", Nome = "Suíça" },
                 new Pais { PaisId = 214, Sigla = "SR", Nome = "Suriname" },
                 new Pais { PaisId = 215, Sigla = "SJ", Nome = "Svalbard e Jan Mayen" },
                 new Pais { PaisId = 216, Sigla = "TH", Nome = "Tailândia" },
                 new Pais { PaisId = 217, Sigla = "TW", Nome = "Taiwan" },
                 new Pais { PaisId = 218, Sigla = "TJ", Nome = "Tajiquistão" },
                 new Pais { PaisId = 219, Sigla = "TZ", Nome = "Tanzânia" },
                 new Pais { PaisId = 220, Sigla = "TF", Nome = "Terras Austrais e Antárticas Francesas (TAAF)" },
                 new Pais { PaisId = 221, Sigla = "IO", Nome = "Território Britânico do Oceano Índico" },
                 new Pais { PaisId = 222, Sigla = "TL", Nome = "TimorLeste" },
                 new Pais { PaisId = 223, Sigla = "TG", Nome = "Togo" },
                 new Pais { PaisId = 224, Sigla = "TK", Nome = "Toquelau" },
                 new Pais { PaisId = 225, Sigla = "TO", Nome = "Tonga" },
                 new Pais { PaisId = 226, Sigla = "TT", Nome = "Trindade e Tobago" },
                 new Pais { PaisId = 227, Sigla = "TN", Nome = "Tunísia" },
                 new Pais { PaisId = 228, Sigla = "TC", Nome = "Turks e Caicos" },
                 new Pais { PaisId = 229, Sigla = "TM", Nome = "Turquemenistão" },
                 new Pais { PaisId = 230, Sigla = "TR", Nome = "Turquia" },
                 new Pais { PaisId = 231, Sigla = "TV", Nome = "Tuvalu" },
                 new Pais { PaisId = 232, Sigla = "UA", Nome = "Ucrânia" },
                 new Pais { PaisId = 233, Sigla = "UG", Nome = "Uganda" },
                 new Pais { PaisId = 234, Sigla = "UY", Nome = "Uruguai" },
                 new Pais { PaisId = 235, Sigla = "UZ", Nome = "Usbequistão" },
                 new Pais { PaisId = 236, Sigla = "VU", Nome = "Vanuatu" },
                 new Pais { PaisId = 237, Sigla = "VA", Nome = "Vaticano" },
                 new Pais { PaisId = 238, Sigla = "VE", Nome = "Venezuela" },
                 new Pais { PaisId = 239, Sigla = "VN", Nome = "Vietname" },
                 new Pais { PaisId = 240, Sigla = "VI", Nome = "Virgens Americanas, Ilhas" },
                 new Pais { PaisId = 241, Sigla = "VG", Nome = "Virgens Britânicas, Ilhas" },
                 new Pais { PaisId = 242, Sigla = "WF", Nome = "Wallis e Futuna" },
                 new Pais { PaisId = 243, Sigla = "ZM", Nome = "Zâmbia" },
                 new Pais { PaisId = 244, Sigla = "ZW", Nome = "Zimbabwe" }
             );


            builder.Entity<Laboratorio>().HasData(
                new Laboratorio { LaboratorioId = 1, Nome = "Sinovac", PaisId = 49 },
                new Laboratorio { LaboratorioId = 2, Nome = "Sinopharm", PaisId = 49 },
                new Laboratorio { LaboratorioId = 3, Nome = "Instituto de Biologia Médica da Academia Chinesa de Ciencias Médicas", PaisId = 49 },
                new Laboratorio { LaboratorioId = 4, Nome = "Instituto de Pesquisa Para Problemas de Segurança Biológica", PaisId = 44 },
                new Laboratorio { LaboratorioId = 5, Nome = "Bharat Biotech", PaisId = 108 },
                new Laboratorio { LaboratorioId = 6, Nome = "Universidade de Oxford/AstraZaneca", PaisId = 186 },
                new Laboratorio { LaboratorioId = 7, Nome = "CanSino", PaisId = 49 },
                new Laboratorio { LaboratorioId = 8, Nome = "Instituto de Pesquisa Gamaleya", PaisId = 190 },
                new Laboratorio { LaboratorioId = 9, Nome = "Johnson", PaisId = 76 },
                new Laboratorio { LaboratorioId = 10, Nome = "Moderna", PaisId = 76 },
                new Laboratorio { LaboratorioId = 11, Nome = "Pfizer/BioNTech", PaisId = 76 },
                new Laboratorio { LaboratorioId = 12, Nome = "Bayer/CureVac", PaisId = 5 },
                new Laboratorio { LaboratorioId = 13, Nome = "Zydus Cadila", PaisId = 108 },
                new Laboratorio { LaboratorioId = 14, Nome = "Novavax", PaisId = 76 },
                new Laboratorio { LaboratorioId = 15, Nome = "Anhui Zhifei Longcom e Academia Chinesa de Ciencias Médicas", PaisId = 49 }
            );

            builder.Entity<AgentePatogenico>().HasData(
                new AgentePatogenico { AgentePatogenicoId = 1, Nome = "COVID19", TipoAgentePatogenicoId = 4 }
            );

            builder.Entity<VarianteAgentePatogenico>().HasData(
                new VarianteAgentePatogenico { VarianteAgentePatogenicoId = 1, AgentePatogenicoId = 1, Nome = "B.1.1.7", PrincipaisMutacoes = "N501Y, 69–70del, P681H", Caracteristica = "Mais transmissível", PaisId = 186 },
                new VarianteAgentePatogenico { VarianteAgentePatogenicoId = 2, AgentePatogenicoId = 1, Nome = "B.1351", PrincipaisMutacoes = "N501Y, K417N, E484K", Caracteristica = "Mais transmissível e com possível enfraquecimento da ação dos anticorpos humanos contra o vírus", PaisId = 2 },
                new VarianteAgentePatogenico { VarianteAgentePatogenicoId = 3, AgentePatogenicoId = 1, Nome = "P.1", PrincipaisMutacoes = "N501Y, K417N, E484K", Caracteristica = "Mais transmissível e com possível enfraquecimento da ação dos anticorpos humanos contra o vírus", PaisId = 33 },
                new VarianteAgentePatogenico { VarianteAgentePatogenicoId = 4, AgentePatogenicoId = 1, Nome = "P.2", PrincipaisMutacoes = "E484K", Caracteristica = "Possível enfraquecimento da ação dos anticorpos humanos contra o vírus", PaisId = 33 },
                new VarianteAgentePatogenico { VarianteAgentePatogenicoId = 5, AgentePatogenicoId = 1, Nome = "B.1.1.207", PrincipaisMutacoes = "P681H", Caracteristica = "Desconhecido", PaisId = 163 }

            );


            builder.Entity<Vacina>().HasData(
                new Vacina { VacinaId = 1, Nome = "CoronaVac", TipoVacinaId = 1, LaboratorioId = 1, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 2, Nome = "BBIBPCorV", TipoVacinaId = 1, LaboratorioId = 2, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 3, Nome = "Desconhecido", TipoVacinaId = 1, LaboratorioId = 3, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 4, Nome = "QzCovidin", TipoVacinaId = 1, LaboratorioId = 4, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 5, Nome = "Covaxin", TipoVacinaId = 1, LaboratorioId = 5, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 6, Nome = "AZD1222", TipoVacinaId = 2, LaboratorioId = 6, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 7, Nome = "AD5nCov", TipoVacinaId = 2, LaboratorioId = 7, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 8, Nome = "Sputnik V", TipoVacinaId = 2, LaboratorioId = 8, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 9, Nome = "Ad26 SARSCoV2", TipoVacinaId = 2, LaboratorioId = 9, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 10, Nome = "mRNA 1273", TipoVacinaId = 3, LaboratorioId = 10, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 11, Nome = "BNT162", TipoVacinaId = 3, LaboratorioId = 11, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 12, Nome = "CVnCoV", TipoVacinaId = 3, LaboratorioId = 12, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 13, Nome = "ZycovD", TipoVacinaId = 3, LaboratorioId = 13, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 14, Nome = "NVXCoV2373", TipoVacinaId = 4, LaboratorioId = 14, AgentePatogenicoId = 1 },
                new Vacina { VacinaId = 15, Nome = "RBDDimer", TipoVacinaId = 4, LaboratorioId = 15, AgentePatogenicoId = 1 }
            );


        }

    }
}
