namespace mooshak_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SorryGuys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        studentId = c.String(),
                        courseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentCourses");
        }
    }
}
