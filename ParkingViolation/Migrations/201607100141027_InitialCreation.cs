namespace ParkingViolation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        FirstMidName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 10),
                        EmailAddress = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PersonID)
                .Index(t => t.PhoneNumber, unique: true, name: "PhoneNumberIndex");
            
            CreateTable(
                "dbo.Violation",
                c => new
                    {
                        State = c.String(nullable: false, maxLength: 2),
                        NumberPlate = c.String(nullable: false, maxLength: 6),
                        ViolationID = c.Int(nullable: false, identity: true),
                        ViolationDateAndTime = c.DateTime(nullable: false),
                        OfficerID = c.Int(nullable: false),
                        Comments = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ViolationID)
                .ForeignKey("dbo.Person", t => t.OfficerID, cascadeDelete: false)
                .ForeignKey("dbo.Vehicle", t => new { t.State, t.NumberPlate }, cascadeDelete: true)
                .Index(t => new { t.State, t.NumberPlate })
                .Index(t => t.OfficerID);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        State = c.String(nullable: false, maxLength: 2),
                        NumberPlate = c.String(nullable: false, maxLength: 6),
                        VolunteerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.State, t.NumberPlate })
                .ForeignKey("dbo.Person", t => t.VolunteerID, cascadeDelete: true)
                .Index(t => t.VolunteerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Violation", new[] { "State", "NumberPlate" }, "dbo.Vehicle");
            DropForeignKey("dbo.Vehicle", "VolunteerID", "dbo.Person");
            DropForeignKey("dbo.Violation", "OfficerID", "dbo.Person");
            DropIndex("dbo.Vehicle", new[] { "VolunteerID" });
            DropIndex("dbo.Violation", new[] { "OfficerID" });
            DropIndex("dbo.Violation", new[] { "State", "NumberPlate" });
            DropIndex("dbo.Person", "PhoneNumberIndex");
            DropTable("dbo.Vehicle");
            DropTable("dbo.Violation");
            DropTable("dbo.Person");
        }
    }
}
