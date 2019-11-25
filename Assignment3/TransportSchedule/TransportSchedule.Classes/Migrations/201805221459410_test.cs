namespace TransportSchedule.Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StationId = c.Int(nullable: false),
                        Description = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.StationId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StationId = c.Int(nullable: false),
                        TimeFromOrigin = c.Int(nullable: false),
                        TimeFromDest = c.Int(nullable: false),
                        Route_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.Route_Id)
                .Index(t => t.StationId)
                .Index(t => t.Route_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favourites", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RouteStations", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.RouteStations", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Favourites", "StationId", "dbo.Stations");
            DropIndex("dbo.RouteStations", new[] { "Route_Id" });
            DropIndex("dbo.RouteStations", new[] { "StationId" });
            DropIndex("dbo.Favourites", new[] { "User_Id" });
            DropIndex("dbo.Favourites", new[] { "StationId" });
            DropTable("dbo.Users");
            DropTable("dbo.RouteStations");
            DropTable("dbo.Routes");
            DropTable("dbo.Stations");
            DropTable("dbo.Favourites");
        }
    }
}
