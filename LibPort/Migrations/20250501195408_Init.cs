using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibPort.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Username = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowingRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    RequestorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequests_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequests_Users_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowingRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequestDetails_BookBorrowingRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "BookBorrowingRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequestDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Comedy" },
                    { 2, "Documentary" },
                    { 3, "Drama" },
                    { 4, "Romance" },
                    { 5, "Action" },
                    { 6, "Fantasy" },
                    { 7, "Horror" },
                    { 8, "Crime" },
                    { 9, "Mystery" },
                    { 10, "Adventure" },
                    { 11, "Western" },
                    { 12, "Animation" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "Quantity", "Title", "Total" },
                values: new object[,]
                {
                    { new Guid("0d60e77e-d9a6-4d47-84b2-7058b265b963"), "Florencio Lackey", 6, "Nontraumatic rupture of muscle", 3, "Man Called Peter, A", 3 },
                    { new Guid("12b4719a-14f7-490f-b5d5-19104de89ff9"), "Lanny Florez", 9, "Secondary malignant neoplasm of bone", 5, "Sunset Park", 5 },
                    { new Guid("14b35443-c5cc-44bb-9823-12ff1eaee847"), "Jaimee Gandara", 4, "Intestinal volvulus", 7, "Parallax View, The", 7 },
                    { new Guid("15ae6a7e-93b5-4e60-8a3c-328c06f2094e"), "Ramonita Hatzell", 12, "Acute osteomyelitis", 7, "Bossa Nova", 7 },
                    { new Guid("2abbdca7-df70-498c-97ed-2c78d1a24fcb"), "Tami Daignault", 7, "Postoperative infection", 10, "Samantha: An American Girl Holiday", 10 },
                    { new Guid("5083e315-fba5-4c27-8a24-6f574d3563ce"), "Laurence Dinsmore", 11, "Anxiety disorder", 6, "Rainbow Thief, The", 6 },
                    { new Guid("636f189e-3c66-4b81-99c2-5efb858adf4e"), "Jarrett Leasure", 1, "Dysplasia of vagina", 4, "Disaster Zone: Volcano in New York", 4 },
                    { new Guid("6a2ecb2f-ec0b-4e27-b6f8-f8f9842ce4a2"), "Rebeca Mcsherry", 4, "Gout, unspecified", 10, "Inside Job", 10 },
                    { new Guid("7b75b12d-3dc4-465a-ae54-bcd0fe2dc57a"), "Eddy Marney", 5, "Enthesopathy of hip region", 8, "Police Story", 8 },
                    { new Guid("9c57b070-22f4-4d6f-97c4-3ac3a1bd6cc7"), "Maris Burgett", 2, "Heatstroke", 9, "Phantom, The", 9 },
                    { new Guid("9c961726-f1d0-41f3-8c42-e8aa36b2fc1c"), "Antwan Hewins", 2, "Dystrophy of retina", 4, "Grumpier Old Men", 4 },
                    { new Guid("9cfc33e9-fd14-4d4d-9bb9-4cfd1405a2b9"), "Santo Ashbrook", 3, "Salter-Harris Type III physeal fracture of lower end of radius", 9, "Other Side of the Bed, The (Lado de la cama, El)", 9 },
                    { new Guid("a55f0c23-b173-4ce5-a67d-9d2630597e7e"), "Sharie Giauque", 1, "Gonococcal infection of lower genitourinary tract", 6, "Book of Life, The", 6 },
                    { new Guid("af4ff5db-9c95-4904-8261-826a3cc4db0d"), "Blair Galvis", 1, "Separation of muscle (nontraumatic)", 3, "Free Zone", 3 },
                    { new Guid("c8660f77-c183-4f8c-8f99-6e8b7c55cc06"), "Wilfred Paquin", 7, "Burn of unspecified degree", 1, "Moloch", 1 },
                    { new Guid("d0069c60-1bb3-49f3-8307-bd5ed4a50544"), "Myrtis Reinhard", 1, "Chronic periodontitis", 5, "Salting the Battlefield", 5 },
                    { new Guid("d14feee2-e37d-471f-9eda-7ce87977fd78"), "Luigi Cash", 1, "Acute tonsillitis", 1, "Cast a Dark Shadow (Angel)", 1 },
                    { new Guid("eaf263b6-cb6a-40b6-a490-81e37437f923"), "Devin Bramwell", 1, "Venous complication", 2, "Lego Star Wars: The Empire Strikes Out", 2 },
                    { new Guid("f34d64ed-786d-4035-8da5-436979306744"), "Crystie Delgadillo", 1, "Hypermobility of coccyx", 2, "Never Too Young to Die", 2 },
                    { new Guid("fc994b85-01e0-4660-96f3-c7cf75b59e02"), "Cortney Mellen", 1, "Preterm labor", 8, "Joy Ride 2: Dead Ahead", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_BookId",
                table: "BookBorrowingRequestDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_RequestId",
                table: "BookBorrowingRequestDetails",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequests_ApproverId",
                table: "BookBorrowingRequests",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequests_RequestorId",
                table: "BookBorrowingRequests",
                column: "RequestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBorrowingRequestDetails");

            migrationBuilder.DropTable(
                name: "BookBorrowingRequests");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
