using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    private static string InitialSql = """
        INSERT INTO `Routes` VALUES (1,4.50,'Siófok kelet','Siófok-Sóstó','DL'),(2,1.60,'Balatonmáriafürdő kelet','Balatonfenyves nyugat','RÁDIÓ 1'),(3,3.10,'Ábrahámhegy','Badacsonyörs Varga pincészet','VARGA'),(4,2.90,'Balatonberény fogadó','Balatonberény','SAUCONY'),(5,3.70,'Balatonalakli','Zánka Unk','BOROTALCO'),(6,3.80,'Balatonfüred Probio','Balatonfüred befutó','ANNAGORA AQUAPARK'),(7,3.00,'Balatonalmádi','Balatonalmádi strand',NULL),(8,4.10,'Csopak','Balatonfüred Probio',NULL),(9,1.80,'Balatonfenyves nyugat','Balatonfenyves','TRILAK'),(10,6.00,'Siófok nyugat','Siófok kelet','DREHER'),(11,1.80,'Révfülöp kelet','Révfülöp nyugat','OTP MOBIL'),(12,1.70,'Gyenesdiás','Keszthely Bikás strand','UNITED SHIPPING'),(13,5.00,'Balatonvilágos','Balatonakarattya','LIPTON'),(14,5.00,'Balatonszemes','Balatonszárszó','KATASZTRÓFAVÉDELEM'),(15,4.80,'Alsóörs','Csopak','BALATONI ÉLMÉNYPARK'),(16,4.30,'Fenékpuszta','Balatonberény fogadó','BALATONMAN TRAINING TEAM'),(17,6.40,'Balatonfűzfő','Balatonalmádi','MVM'),(18,3.10,'Balatonszepezd','Révfülöp kelet','NICOFLEX'),(19,4.50,'Aszófő','Fövenyes','MARKET'),(20,3.40,'Balatongyörök 2','Vonyarcvashegy','BRIDGESTONE'),(21,3.70,'Zánka','Balatonszepezd','PRIMAVERA'),(22,5.30,'Fonyódliget','Balatonboglár','TOYOTA'),(23,5.00,'Badacsonyörs Varga pincészet','Badacsony','MOZGÁSVILÁG'),(24,3.90,'Balatonszárszó','Balatonföldvár',NULL),(25,5.50,'Balatonkenese','Balatonfűzfő','KNORR BREMSE'),(26,2.80,'Balatongyörök','Balatongyörök 2','SWISS'),(27,2.20,'Vonyarcvashegy','Gyenesdiás','GUKMIFLEX'),(28,1.90,'Balatonboglár','Balatohboglár kelet','FILMIO'),(29,5.20,'Balatonmáriafürdő nyugat','Balatonmáriafürdő kelet','SMARTEQ'),(30,5.60,'Balatonalmádi strand','Alsóörs','SAMSUNG EXPERIENCE STORE'),(31,2.20,'Balatonvilágos parti út','Balatonvilágos',NULL),(32,5.20,'Balatonlelle kelet','Balatonszemes','ECOFAMILY'),(33,4.60,'Balatonfenyves','Alsóbélatelep','GYERMELYI'),(34,3.10,'Keszthely Bikás strand','Keszthely','TESCO'),(35,3.20,'Fonyód','Fonyódliget','LAVAZZA'),(36,4.50,'Fövenyes','Balatonalakli',NULL),(37,2.00,'Balatonlelle nyugat','Balatonlelle kelet','BRFK'),(38,3.30,'Siófok-Sóstó','Balatonvilágos parti út','MOM SPORT'),(39,7.60,'Szigliget','Balatongyörök','NN'),(40,2.50,'Balatohboglár kelet','Balatonlelle nyugat','HELL'),(41,3.80,'Balatonberény','Balatonmáriafürdő nyugat','SPIRIT HOTEL'),(42,4.30,'Szántód','Zamárdi','MOL NAGYON BALATON'),(43,6.10,'Balatonakarattya','Balatonkenese','BWT'),(44,5.00,'Badacsony','Badacsonytördemic','KORONÁS CUKOR'),(45,3.30,'Alsóbélatelep','Fonyód','NN'),(46,5.30,'Révfülöp nyugat','Ábrahámhegy','MEDVE SAJT'),(47,3.40,'Badacsonytördemic','Szigliget',NULL),(48,5.30,'Zamárdi','Balatonszéplak','ENERGOFISH'),(49,4.80,'Keszthely','Fenékpuszta',NULL),(50,2.90,'Zánka Unk','Zánka','SUZUKI'),(51,1.60,'Balatonföldvár strand','Szántód','ALDI'),(52,1.70,'Balatonföldvár','Balatonföldvár strand','MOM SPORT'),(53,7.00,'Rajt','Aszófő','NHKV'),(54,2.20,'Balatonszéplak','Siófok nyugat','TESCO II');
        """;

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Routes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                Distance = table.Column<double>(type: "double", nullable: false),
                StartingLocation = table.Column<string>(type: "longtext", nullable: true),
                ArrivalLocation = table.Column<string>(type: "longtext", nullable: true),
                RoutePartName = table.Column<string>(type: "longtext", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Routes", x => x.Id);
            })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                Username = table.Column<string>(type: "longtext", nullable: true),
                Password = table.Column<string>(type: "longtext", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.Sql(InitialSql);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Routes");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
