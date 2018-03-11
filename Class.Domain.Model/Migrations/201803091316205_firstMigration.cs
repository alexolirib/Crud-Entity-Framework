namespace Class.Domain.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Note = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evaluations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Evaluations", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Evaluations", new[] { "CourseId" });
            DropIndex("dbo.Evaluations", new[] { "StudentId" });
            DropIndex("dbo.Courses", new[] { "AuthorId" });
            DropTable("dbo.Students");
            DropTable("dbo.Evaluations");
            DropTable("dbo.Courses");
            DropTable("dbo.Authors");
        }
    }
}
