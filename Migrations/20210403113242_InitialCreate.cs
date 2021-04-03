using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CVC19.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Sigla = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "TipoAgentePatogenico",
                columns: table => new
                {
                    TipoAgentePatogenicoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAgentePatogenico", x => x.TipoAgentePatogenicoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoVacina",
                columns: table => new
                {
                    TipoVacinaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVacina", x => x.TipoVacinaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    LaboratorioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PaisId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.LaboratorioId);
                    table.ForeignKey(
                        name: "FK_Laboratorio_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentePatogenico",
                columns: table => new
                {
                    AgentePatogenicoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TipoAgentePatogenicoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentePatogenico", x => x.AgentePatogenicoId);
                    table.ForeignKey(
                        name: "FK_AgentePatogenico_TipoAgentePatogenico_TipoAgentePatogenicoId",
                        column: x => x.TipoAgentePatogenicoId,
                        principalTable: "TipoAgentePatogenico",
                        principalColumn: "TipoAgentePatogenicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacina",
                columns: table => new
                {
                    VacinaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LaboratorioId = table.Column<int>(type: "integer", nullable: false),
                    TipoVacinaId = table.Column<int>(type: "integer", nullable: false),
                    AgentePatogenicoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacina", x => x.VacinaId);
                    table.ForeignKey(
                        name: "FK_Vacina_AgentePatogenico_AgentePatogenicoId",
                        column: x => x.AgentePatogenicoId,
                        principalTable: "AgentePatogenico",
                        principalColumn: "AgentePatogenicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacina_Laboratorio_LaboratorioId",
                        column: x => x.LaboratorioId,
                        principalTable: "Laboratorio",
                        principalColumn: "LaboratorioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacina_TipoVacina_TipoVacinaId",
                        column: x => x.TipoVacinaId,
                        principalTable: "TipoVacina",
                        principalColumn: "TipoVacinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VarianteAgentePatogenico",
                columns: table => new
                {
                    VarianteAgentePatogenicoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PrincipaisMutacoes = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Caracteristica = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AgentePatogenicoId = table.Column<int>(type: "integer", nullable: false),
                    PaisId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VarianteAgentePatogenico", x => x.VarianteAgentePatogenicoId);
                    table.ForeignKey(
                        name: "FK_VarianteAgentePatogenico_AgentePatogenico_AgentePatogenicoId",
                        column: x => x.AgentePatogenicoId,
                        principalTable: "AgentePatogenico",
                        principalColumn: "AgentePatogenicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VarianteAgentePatogenico_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3", "330a116f-6859-4520-966b-b7ede22dbdb2", "GESTOR_PATOGENO", "GESTOR_PATOGENO" },
                    { "2", "8c5876e9-a4f8-4007-89d3-b5a9b09e7182", "GESTOR_VACINA", "GESTOR_VACINA" },
                    { "1", "095a6715-6b9a-41ab-933c-e4a8c067f220", "ADMIN", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "95c36366-0c04-41fc-b317-f77655da019d", 0, "62e0cbc7-8747-4dbb-8bdd-e256c5b6d3cf", "adminstrador@gestaopandemia.com.br", false, false, null, "ADMINSTRADOR@GESTAOPANDEMIA.COM.BR", "ADMINSTRADOR@GESTAOPANDEMIA.COM.BR", "AQAAAAEAACcQAAAAEBhFLa833zCa3xvqBMqCpq4eJN2Ub7k91Dqdyh0Fgf2RuX3KVcw6HKFZpXCRXEoWTA==", null, false, "YWCTGIE4H4U22NNV5K4NUA6EH5MQYG5B", false, "adminstrador@gestaopandemia.com.br" },
                    { "1ff6e1f7-2049-434b-8e5a-93d454203652", 0, "0799389d-af42-4530-8b63-4efff19fb702", "gestor.patogeno@gestaopandemia.com.br", false, false, null, "GESTOR.PATOGENO@GESTAOPANDEMIA.COM.BR", "GESTOR.PATOGENO@GESTAOPANDEMIA.COM.BR", "AQAAAAEAACcQAAAAEDnGJexv5zVzdMX9JOmG2bzgpcOLKfy9ra6cMqZr8JGIdxq7PxqR1lkCU+Pg0Gqecw==", null, false, "KNIHZXZV42FGMK7XZDGEIX6P6QPUICGU", false, "gestor.patogeno@gestaopandemia.com.br" },
                    { "3872c9f1-555b-486f-a5e5-5f0de852b677", 0, "efedc7fc-f4b6-444e-be24-61aef4f24f0e", "gestor.vacina@gestaopandemia.com.br", false, false, null, "GESTOR.VACINA@GESTAOPANDEMIA.COM.BR", "GESTOR.VACINA@GESTAOPANDEMIA.COM.BR", "AQAAAAEAACcQAAAAEEhruVTtYGoBjbjyyKVnIx10uuPr5Me0VYWkUwPXZynAOSpAZxlk8Umwj92FDimzgw==", null, false, "Z2YZBTQXYJYYTIN2ORODRQWI3RPJN5ZS", false, "gestor.vacina@gestaopandemia.com.br" }
                });

            migrationBuilder.InsertData(
                table: "Pais",
                columns: new[] { "PaisId", "Nome", "Sigla" },
                values: new object[,]
                {
                    { 168, "Nova Zelândia (Aotearoa)", "NZ" },
                    { 167, "Nova Caledónia", "NC" },
                    { 166, "Noruega", "NO" },
                    { 165, "Norfolk, Ilha", "NF" },
                    { 164, "Niue", "NU" },
                    { 161, "Nicarágua", "NI" },
                    { 162, "Níger", "NE" },
                    { 169, "Oman", "OM" },
                    { 160, "Nepal", "NP" },
                    { 159, "Nauru", "NR" },
                    { 163, "Nigéria", "NG" },
                    { 170, "Países Baixos (Holanda)", "NL" },
                    { 173, "Panamá", "PA" },
                    { 172, "Palestina", "PS" },
                    { 158, "Namíbia", "NA" },
                    { 174, "PapuaNova Guiné", "PG" },
                    { 175, "Paquistão", "PK" },
                    { 176, "Paraguai", "PY" },
                    { 177, "Peru", "PE" },
                    { 178, "Pitcairn", "PN" },
                    { 179, "Polinésia Francesa", "PF" },
                    { 180, "Polónia", "PL" },
                    { 181, "Porto Rico", "PR" },
                    { 182, "Portugal", "PT" },
                    { 183, "Qatar", "QA" },
                    { 171, "Palau", "PW" },
                    { 157, "Montserrat", "MS" },
                    { 154, "Mónaco", "MC" },
                    { 155, "Mongólia", "MN" },
                    { 128, "Liechtenstein", "LI" },
                    { 130, "Luxemburgo", "LU" },
                    { 131, "Macau", "MO" },
                    { 132, "Macedónia, República da", "MK" },
                    { 133, "Madagáscar", "MG" },
                    { 134, "Malásia", "MY" },
                    { 135, "Malawi", "MW" },
                    { 136, "Maldivas", "MV" },
                    { 137, "Mali", "ML" },
                    { 138, "Malta", "MT" },
                    { 139, "Malvinas, Ilhas (Falkland)", "FK" },
                    { 140, "Man, Ilha de", "IM" },
                    { 156, "Montenegro", "ME" },
                    { 141, "Marianas Setentrionais", "MP" },
                    { 143, "Marshall, Ilhas", "MH" },
                    { 144, "Martinica", "MQ" },
                    { 145, "Maurícia", "MU" },
                    { 146, "Mauritânia", "MR" },
                    { 147, "Mayotte", "YT" },
                    { 148, "Menores Distantes dos Estados Unidos, Ilhas", "UM" },
                    { 149, "México", "MX" },
                    { 150, "Myanmar (antiga Birmânia)", "MM" },
                    { 151, "Micronésia, Estados Federados da", "FM" },
                    { 152, "Moçambique", "MZ" },
                    { 153, "Moldávia", "MD" },
                    { 184, "Quénia", "KE" },
                    { 142, "Marrocos", "MA" },
                    { 185, "Quirguistão", "KG" },
                    { 188, "Roménia", "RO" },
                    { 187, "Reunião", "RE" },
                    { 219, "Tanzânia", "TZ" },
                    { 220, "Terras Austrais e Antárticas Francesas (TAAF)", "TF" },
                    { 221, "Território Britânico do Oceano Índico", "IO" },
                    { 222, "TimorLeste", "TL" },
                    { 223, "Togo", "TG" },
                    { 224, "Toquelau", "TK" },
                    { 225, "Tonga", "TO" },
                    { 226, "Trindade e Tobago", "TT" },
                    { 227, "Tunísia", "TN" },
                    { 228, "Turks e Caicos", "TC" },
                    { 229, "Turquemenistão", "TM" },
                    { 230, "Turquia", "TR" },
                    { 218, "Tajiquistão", "TJ" },
                    { 231, "Tuvalu", "TV" },
                    { 233, "Uganda", "UG" },
                    { 234, "Uruguai", "UY" },
                    { 235, "Usbequistão", "UZ" },
                    { 236, "Vanuatu", "VU" },
                    { 237, "Vaticano", "VA" },
                    { 238, "Venezuela", "VE" },
                    { 239, "Vietname", "VN" },
                    { 240, "Virgens Americanas, Ilhas", "VI" },
                    { 241, "Virgens Britânicas, Ilhas", "VG" },
                    { 242, "Wallis e Futuna", "WF" },
                    { 243, "Zâmbia", "ZM" },
                    { 244, "Zimbabwe", "ZW" },
                    { 232, "Ucrânia", "UA" },
                    { 217, "Taiwan", "TW" },
                    { 216, "Tailândia", "TH" },
                    { 215, "Svalbard e Jan Mayen", "SJ" },
                    { 127, "Líbia", "LY" },
                    { 189, "Ruanda", "RW" },
                    { 190, "Rússia", "RU" },
                    { 191, "Saara Ocidental", "EH" },
                    { 192, "Samoa Americana", "AS" },
                    { 193, "Samoa (Samoa Ocidental)", "WS" },
                    { 194, "Saint Pierre et Miquelon", "PM" },
                    { 195, "Salomão, Ilhas", "SB" },
                    { 196, "São Cristóvão e Névis (Saint Kitts e Nevis)", "KN" },
                    { 197, "San Marino", "SM" },
                    { 198, "São Tomé e Príncipe", "ST" },
                    { 199, "São Vicente e Granadinas", "VC" },
                    { 200, "Santa Helena", "SH" },
                    { 201, "Santa Lúcia", "LC" },
                    { 202, "Senegal", "SN" },
                    { 203, "Serra Leoa", "SL" },
                    { 204, "Sérvia", "RS" },
                    { 205, "Seychelles", "SC" },
                    { 206, "Singapura", "SG" },
                    { 207, "Síria", "SY" },
                    { 208, "Somália", "SO" },
                    { 209, "Sri Lanka", "LK" },
                    { 210, "Suazilândia", "SZ" },
                    { 211, "Sudão", "SD" },
                    { 212, "Suécia", "SE" },
                    { 213, "Suíça", "CH" },
                    { 214, "Suriname", "SR" },
                    { 186, "Reino Unido da GrãBretanha e Irlanda do Norte", "GB" },
                    { 126, "Libéria", "LR" },
                    { 129, "Lituânia", "LT" },
                    { 124, "Letónia", "LV" },
                    { 34, "Brunei", "BN" },
                    { 35, "Bulgária", "BG" },
                    { 36, "Burkina Faso", "BF" },
                    { 37, "Burundi", "BI" },
                    { 38, "Butão", "BT" },
                    { 39, "Cabo Verde", "CV" },
                    { 40, "Cambodja", "KH" },
                    { 41, "Camarões", "CM" },
                    { 42, "Canadá", "CA" },
                    { 43, "Cayman, Ilhas", "KY" },
                    { 44, "Cazaquistão", "KZ" },
                    { 45, "Centroafricana, República", "CF" },
                    { 46, "Chade", "TD" },
                    { 47, "Checa, República", "CZ" },
                    { 48, "Chile", "CL" },
                    { 49, "China", "CN" },
                    { 50, "Chipre", "CY" },
                    { 51, "Christmas, Ilha", "CX" },
                    { 52, "Cocos, Ilhas", "CC" },
                    { 53, "Colômbia", "CO" },
                    { 54, "Comores", "KM" },
                    { 55, "Congo, República do", "CG" },
                    { 56, "Congo, República Democrática do (antigo Zaire)", "CD" },
                    { 57, "Cook, Ilhas", "CK" },
                    { 125, "Líbano", "LB" },
                    { 59, "Coreia, República Democrática da (Coreia do Norte)", "KP" },
                    { 60, "Costa do Marfim", "CI" },
                    { 33, "Brasil", "BR" },
                    { 32, "Bouvet, Ilha", "BV" },
                    { 31, "Botswana", "BW" },
                    { 30, "BósniaHerzegovina", "BA" },
                    { 2, "África do Sul", "ZA" },
                    { 3, "Åland, Ilhas", "AX" },
                    { 4, "Albânia", "AL" },
                    { 5, "Alemanha", "DE" },
                    { 6, "Andorra", "AD" },
                    { 7, "Angola", "AO" },
                    { 8, "Anguilla", "AI" },
                    { 9, "Antártica", "AQ" },
                    { 10, "Antigua e Barbuda", "AG" },
                    { 11, "Antilhas Holandesas", "AN" },
                    { 12, "Arábia Saudita", "SA" },
                    { 13, "Argélia", "DZ" },
                    { 14, "Argentina", "AR" },
                    { 61, "Costa Rica", "CR" },
                    { 15, "Arménia", "AM" },
                    { 17, "Austrália", "AU" },
                    { 18, "Áustria", "AT" },
                    { 19, "Azerbeijão", "AZ" },
                    { 20, "Bahamas", "BS" },
                    { 21, "Bahrain", "BH" },
                    { 22, "Bangladesh", "BD" },
                    { 23, "Barbados", "BB" },
                    { 24, "Bélgica", "BE" },
                    { 25, "Belize", "BZ" },
                    { 26, "Benin", "BJ" },
                    { 27, "Bermuda", "BM" },
                    { 28, "BieloRússia", "BY" },
                    { 29, "Bolívia", "BO" },
                    { 16, "Aruba", "AW" },
                    { 62, "Croácia", "HR" },
                    { 58, "Coreia do Sul", "KR" },
                    { 64, "Dinamarca", "DK" },
                    { 97, "Guiana", "GY" },
                    { 63, "Cuba", "CU" },
                    { 99, "GuinéBissau", "GW" },
                    { 100, "GuinéConacri", "GN" },
                    { 101, "Guiné Equatorial", "GQ" },
                    { 102, "Haiti", "HT" },
                    { 103, "Heard e Ilhas McDonald, Ilha", "HM" },
                    { 104, "Honduras", "HN" },
                    { 105, "Hong Kong", "HK" },
                    { 106, "Hungria", "HU" },
                    { 107, "Iémen", "YE" },
                    { 108, "Índia", "IN" },
                    { 109, "Indonésia", "ID" },
                    { 110, "Iraque", "IQ" },
                    { 111, "Irão", "IR" },
                    { 112, "Irlanda", "IE" },
                    { 113, "Islândia", "IS" },
                    { 114, "Israel", "IL" },
                    { 115, "Itália", "IT" },
                    { 116, "Jamaica", "JM" },
                    { 117, "Japão", "JP" },
                    { 118, "Jersey", "JE" },
                    { 119, "Jordânia", "JO" },
                    { 120, "Kiribati", "KI" },
                    { 121, "Kuwait", "KW" },
                    { 122, "Laos", "LA" },
                    { 123, "Lesoto", "LS" },
                    { 96, "Guernsey", "GG" },
                    { 95, "Guatemala", "GT" },
                    { 98, "Guiana Francesa", "GF" },
                    { 93, "Guadeloupe", "GP" },
                    { 65, "Djibouti", "DJ" },
                    { 66, "Dominica", "DM" },
                    { 67, "Dominicana, República", "DO" },
                    { 68, "Egipto", "EG" },
                    { 69, "El Salvador", "SV" },
                    { 70, "Emiratos Árabes Unidos", "AE" },
                    { 71, "Equador", "EC" },
                    { 72, "Eritreia", "ER" },
                    { 73, "Eslováquia", "SK" },
                    { 94, "Guam", "GU" },
                    { 75, "Espanha", "ES" },
                    { 76, "Estados Unidos da América", "US" },
                    { 77, "Estónia", "EE" },
                    { 78, "Etiópia", "ET" },
                    { 74, "Eslovénia", "SI" },
                    { 80, "Fiji", "FJ" },
                    { 92, "Gronelândia", "GL" },
                    { 91, "Grenada", "GD" },
                    { 90, "Grécia", "GR" },
                    { 89, "Gibraltar", "GI" },
                    { 79, "Faroe, Ilhas", "FO" },
                    { 88, "Geórgia do Sul e Sandwich do Sul, Ilhas", "GS" },
                    { 1, "Afeganistão", "AF" },
                    { 86, "Gana", "GH" },
                    { 85, "Gâmbia", "GM" },
                    { 84, "Gabão", "GA" },
                    { 83, "França", "FR" },
                    { 82, "Finlândia", "FI" },
                    { 81, "Filipinas", "PH" },
                    { 87, "Geórgia", "GE" }
                });

            migrationBuilder.InsertData(
                table: "TipoAgentePatogenico",
                columns: new[] { "TipoAgentePatogenicoId", "Nome" },
                values: new object[,]
                {
                    { 1, "Bactéria" },
                    { 2, "Fungo" },
                    { 4, "Vírus" },
                    { 3, "Protozoário" }
                });

            migrationBuilder.InsertData(
                table: "TipoVacina",
                columns: new[] { "TipoVacinaId", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Usa o patógeno completo e inativo (\"Morto\"). A vacina tem todas as proteínas do vírus", "Patógeno Inativado" },
                    { 2, "Usa um outro patógeno para carregar o material genético para dentro da célula", "Vetor Patógeno" },
                    { 3, "Usa o material genético (RNA) do patógeno para que fornecer as instruções para que a célula produz a proteína do patógeno", "RNA" },
                    { 4, "Fornece a proteína para estimular o sistema imunológico", "Subunidade de Proteínas" }
                });

            migrationBuilder.InsertData(
                table: "AgentePatogenico",
                columns: new[] { "AgentePatogenicoId", "Nome", "TipoAgentePatogenicoId" },
                values: new object[] { 1, "COVID19", 4 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "95c36366-0c04-41fc-b317-f77655da019d" },
                    { "3", "1ff6e1f7-2049-434b-8e5a-93d454203652" },
                    { "2", "3872c9f1-555b-486f-a5e5-5f0de852b677" }
                });

            migrationBuilder.InsertData(
                table: "Laboratorio",
                columns: new[] { "LaboratorioId", "Nome", "PaisId" },
                values: new object[,]
                {
                    { 8, "Instituto de Pesquisa Gamaleya", 190 },
                    { 6, "Universidade de Oxford/AstraZaneca", 186 },
                    { 13, "Zydus Cadila", 108 },
                    { 5, "Bharat Biotech", 108 },
                    { 14, "Novavax", 76 },
                    { 11, "Pfizer/BioNTech", 76 },
                    { 9, "Johnson", 76 },
                    { 15, "Anhui Zhifei Longcom e Academia Chinesa de Ciencias Médicas", 49 },
                    { 7, "CanSino", 49 },
                    { 3, "Instituto de Biologia Médica da Academia Chinesa de Ciencias Médicas", 49 },
                    { 2, "Sinopharm", 49 },
                    { 1, "Sinovac", 49 },
                    { 4, "Instituto de Pesquisa Para Problemas de Segurança Biológica", 44 },
                    { 10, "Moderna", 76 },
                    { 12, "Bayer/CureVac", 5 }
                });

            migrationBuilder.InsertData(
                table: "Vacina",
                columns: new[] { "VacinaId", "AgentePatogenicoId", "LaboratorioId", "Nome", "TipoVacinaId" },
                values: new object[,]
                {
                    { 1, 1, 1, "CoronaVac", 1 },
                    { 15, 1, 15, "RBDDimer", 4 },
                    { 14, 1, 14, "NVXCoV2373", 4 },
                    { 13, 1, 13, "ZycovD", 3 },
                    { 12, 1, 12, "CVnCoV", 3 },
                    { 11, 1, 11, "BNT162", 3 },
                    { 9, 1, 9, "Ad26 SARSCoV2", 2 },
                    { 10, 1, 10, "mRNA 1273", 3 },
                    { 7, 1, 7, "AD5nCov", 2 },
                    { 6, 1, 6, "AZD1222", 2 },
                    { 5, 1, 5, "Covaxin", 1 },
                    { 4, 1, 4, "QzCovidin", 1 },
                    { 3, 1, 3, "Desconhecido", 1 },
                    { 2, 1, 2, "BBIBPCorV", 1 },
                    { 8, 1, 8, "Sputnik V", 2 }
                });

            migrationBuilder.InsertData(
                table: "VarianteAgentePatogenico",
                columns: new[] { "VarianteAgentePatogenicoId", "AgentePatogenicoId", "Caracteristica", "Nome", "PaisId", "PrincipaisMutacoes" },
                values: new object[,]
                {
                    { 4, 1, "Possível enfraquecimento da ação dos anticorpos humanos contra o vírus", "P.2", 33, "E484K" },
                    { 1, 1, "Mais transmissível", "B.1.1.7", 186, "N501Y, 69–70del, P681H" },
                    { 2, 1, "Mais transmissível e com possível enfraquecimento da ação dos anticorpos humanos contra o vírus", "B.1351", 2, "N501Y, K417N, E484K" },
                    { 3, 1, "Mais transmissível e com possível enfraquecimento da ação dos anticorpos humanos contra o vírus", "P.1", 33, "N501Y, K417N, E484K" },
                    { 5, 1, "Desconhecido", "B.1.1.207", 163, "P681H" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentePatogenico_TipoAgentePatogenicoId",
                table: "AgentePatogenico",
                column: "TipoAgentePatogenicoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Laboratorio_PaisId",
                table: "Laboratorio",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacina_AgentePatogenicoId",
                table: "Vacina",
                column: "AgentePatogenicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacina_LaboratorioId",
                table: "Vacina",
                column: "LaboratorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacina_TipoVacinaId",
                table: "Vacina",
                column: "TipoVacinaId");

            migrationBuilder.CreateIndex(
                name: "IX_VarianteAgentePatogenico_AgentePatogenicoId",
                table: "VarianteAgentePatogenico",
                column: "AgentePatogenicoId");

            migrationBuilder.CreateIndex(
                name: "IX_VarianteAgentePatogenico_PaisId",
                table: "VarianteAgentePatogenico",
                column: "PaisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Vacina");

            migrationBuilder.DropTable(
                name: "VarianteAgentePatogenico");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Laboratorio");

            migrationBuilder.DropTable(
                name: "TipoVacina");

            migrationBuilder.DropTable(
                name: "AgentePatogenico");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "TipoAgentePatogenico");
        }
    }
}
