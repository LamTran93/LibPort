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
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
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
                    { new Guid("0053ea56-f57f-4bdf-8cae-e55cdcaf843f"), "Christean Baldree", 6, "Soft and warm corn tortillas, perfect for tacos and burritos.", 81, "Celluloid Closet, The", 81 },
                    { new Guid("00d358c7-ab98-4273-b4c3-3c672cec527d"), "Lolita Worpole", 5, "Compact and portable backpack for day trips and travel.", 17, "Meatballs III", 17 },
                    { new Guid("02809c89-bd9f-4248-9037-c7dac59bce94"), "Jemimah Wilsey", 3, "Hands-free waist pack for carrying essentials while walking your dog.", 11, "The Borderlands", 11 },
                    { new Guid("05b596de-000f-4640-868a-f7ce351fca72"), "Georgianne Seeds", 8, "Gluten-free bread mix made with almond flour.", 8, "Klip (Clip)", 8 },
                    { new Guid("0d60e77e-d9a6-4d47-84b2-7058b265b963"), "Florencio Lackey", 6, "Nontraumatic rupture of muscle", 3, "Man Called Peter, A", 3 },
                    { new Guid("12b4719a-14f7-490f-b5d5-19104de89ff9"), "Lanny Florez", 9, "Secondary malignant neoplasm of bone", 5, "Sunset Park", 5 },
                    { new Guid("13622935-03ef-471d-967a-425b7ee8ade6"), "Idalia Erskin", 2, "Small toolkit with essentials for home repairs.", 10, "New Rose Hotel", 10 },
                    { new Guid("14b35443-c5cc-44bb-9823-12ff1eaee847"), "Jaimee Gandara", 4, "Intestinal volvulus", 7, "Parallax View, The", 7 },
                    { new Guid("15ae6a7e-93b5-4e60-8a3c-328c06f2094e"), "Ramonita Hatzell", 12, "Acute osteomyelitis", 7, "Bossa Nova", 7 },
                    { new Guid("170f7afa-c2df-42df-b4a9-4f522d4ccab8"), "Beverley Lambal", 11, "Wi-Fi camera with motion detection for home security.", 60, "Bad Company", 60 },
                    { new Guid("1aac33d9-836e-4217-8a80-6d3adb072d71"), "Fionnula Readshall", 4, "Oven-roasted potatoes tossed in garlic and parmesan cheese seasoning.", 14, "Persepolis", 14 },
                    { new Guid("1ae97f4c-1003-4923-bd8b-86230e819310"), "Heindrick Walcot", 4, "Frozen zucchini slices coated in parmesan cheese, perfect for baking or frying.", 73, "Beautiful Girls", 73 },
                    { new Guid("1fcb574e-673f-4673-9bb7-2a57f630b220"), "Esme Harsant", 2, "Creamy tahini made from ground sesame seeds.", 54, "New Daughter, The", 54 },
                    { new Guid("23d06134-f7b7-498f-9140-7226f97429de"), "Lulita Littlepage", 11, "Reusable silicone lids for covering food in bowls and storage containers.", 2, "How Do You Know", 2 },
                    { new Guid("2aa246b3-7e77-4e9c-bf1e-4e948a741000"), "Herc McGaffey", 8, "Convenient charging stand for smartphones and devices.", 57, "Small Town, The (a.k.a. The Town) (Kasaba)", 57 },
                    { new Guid("2abbdca7-df70-498c-97ed-2c78d1a24fcb"), "Tami Daignault", 7, "Postoperative infection", 10, "Samantha: An American Girl Holiday", 10 },
                    { new Guid("2e027286-404b-4030-adb5-32b0b1627207"), "Doralia Knowling", 9, "Compact organizer for cosmetics during travel.", 31, "Viva Las Vegas", 31 },
                    { new Guid("3036a334-da9b-444e-b56b-45df1fbfa7b4"), "Gerrard Abramsky", 8, "Infuser for brewing loose-leaf herbal teas easily.", 18, "Righteous Kill", 18 },
                    { new Guid("3af945dc-3b18-4d41-a856-eb92fa59eb80"), "Lucila Tilburn", 4, "Safety vest for pets during nighttime walks.", 49, "Hell Is for Heroes", 49 },
                    { new Guid("4029dd19-9027-4b8d-9646-07547c162193"), "Francene Marriot", 5, "Moist brownie topped with sea salt and caramel drizzle.", 66, "Bounce: Behind the Velvet Rope", 66 },
                    { new Guid("402a2e5e-1186-4c37-9ae6-f448e3f06a40"), "Ingaborg Limb", 11, "Crispy spring rolls filled with vegetables", 44, "Backwood Philosopher (Havukka-ahon ajattelija)", 44 },
                    { new Guid("4640b125-c365-45e4-a8eb-2f4121aadd9e"), "Irita Lomasny", 2, "Crunchy seeds perfect for toppings and baking.", 76, "Men with Guns", 76 },
                    { new Guid("472d00c2-c68e-4003-a266-4cf0bc1d6df9"), "Rudyard Egdal", 9, "Frozen sausage patties made with herbs and spices, perfect for breakfast sandwiches.", 74, "Kim", 74 },
                    { new Guid("4e06bbd1-6b56-410b-a325-f96ecf5f0f8a"), "Clarance Omand", 6, "A pack of assorted nut and protein bars for a quick energy boost.", 47, "Murder in the First", 47 },
                    { new Guid("4eb50b68-0697-414b-a8b5-d96132d536cf"), "Leslie Gloy", 8, "Professional sharpening system for kitchen knives.", 6, "Bullitt", 6 },
                    { new Guid("5083e315-fba5-4c27-8a24-6f574d3563ce"), "Laurence Dinsmore", 11, "Anxiety disorder", 6, "Rainbow Thief, The", 6 },
                    { new Guid("51e3d063-2c80-416e-b4ae-836a3b4ab56f"), "Justus Farran", 5, "Gentle brush for removing loose fur from pets.", 69, "Direct Contact", 69 },
                    { new Guid("562ea69d-8995-45be-9d07-d1149919e526"), "Freddy Lacroix", 3, "Rechargeable LED camping lantern for outdoor use.", 23, "Escape from Dartmoor", 23 },
                    { new Guid("5719b541-9fe4-4230-8491-b2bac997f22c"), "Georgine Pedel", 8, "Programmable pet feeder for scheduled meals.", 43, "Angst", 43 },
                    { new Guid("58fc28e6-4a55-4d1b-b54f-739bef68f838"), "Clay Loker", 10, "Crispy and crunchy snack made from assorted vegetables.", 36, "Pépé le Moko", 36 },
                    { new Guid("60eefe1a-0cab-4cdf-b15c-9d96d142d921"), "Timoteo Jeannel", 7, "Advanced electric toothbrush for effective cleaning.", 46, "American Psycho", 46 },
                    { new Guid("636f189e-3c66-4b81-99c2-5efb858adf4e"), "Jarrett Leasure", 1, "Dysplasia of vagina", 4, "Disaster Zone: Volcano in New York", 4 },
                    { new Guid("677485a2-2823-4d97-a72b-d547f5db552e"), "Josee Iacovucci", 6, "Rechargeable LED lantern ideal for camping and outdoor activities.", 61, "Flirtation Walk", 61 },
                    { new Guid("6a2ecb2f-ec0b-4e27-b6f8-f8f9842ce4a2"), "Rebeca Mcsherry", 4, "Gout, unspecified", 10, "Inside Job", 10 },
                    { new Guid("7b75b12d-3dc4-465a-ae54-bcd0fe2dc57a"), "Eddy Marney", 5, "Enthesopathy of hip region", 8, "Police Story", 8 },
                    { new Guid("80c888ba-4de4-4b69-a870-d97a2c05221c"), "Celia Robjohns", 2, "Crunchy pretzels dipped in rich dark chocolate.", 75, "Last Metro, The (Dernier métro, Le)", 75 },
                    { new Guid("81403f89-497f-4195-8fdb-1a563a26a749"), "Josh Ivanenko", 6, "Deliciously smoked salmon, great for bagels.", 3, "Maria's Lovers", 3 },
                    { new Guid("873878ae-4224-4737-aca6-576ea7a37c2a"), "Dan Effemy", 8, "Spreadable cream cheese with garlic and herbs.", 80, "The Oscar", 80 },
                    { new Guid("87cc034c-af7a-4aa0-81c1-24077e4847c1"), "Nelia Kender", 9, "Concentrated tomato paste, great for sauces.", 52, "Gold", 52 },
                    { new Guid("8818a00c-cbe3-4f4b-a106-7b9455b63860"), "Nadeen Kincaid", 6, "Stay dry with these stylish waterproof rain boots.", 51, "Time and Tide (Seunlau Ngaklau)", 51 },
                    { new Guid("8873bb31-91d2-4fb9-befc-ee186bb78d23"), "Annalee Oakenfield", 3, "Lightweight hair dryer with multiple speed settings.", 41, "Adventure of Sherlock Holmes' Smarter Brother, The", 41 },
                    { new Guid("8983ec49-ef9f-419d-88f0-fea619bc1bd2"), "Brande Pellew", 4, "Comprehensive first aid kit for home or travel emergencies.", 70, "Taxi to the Dark Side", 70 },
                    { new Guid("8d2c9032-539f-4f63-959b-feec2204ae57"), "Nicola Decayette", 11, "Fresh and zesty salsa, perfect for nachos.", 25, "Operation Homecoming: Writing the Wartime Experience", 25 },
                    { new Guid("8e15def4-c718-4c7d-bd97-82873a02d398"), "Cecilius Rein", 9, "Frozen mix of colorful stir-fry veggies.", 33, "Faust", 33 },
                    { new Guid("8eb071ad-37b3-41db-a45d-9adbd7c46f29"), "Kamilah Easey", 9, "Hands-on experience with science and engineering projects.", 82, "Little Girl Who Lives Down the Lane, The", 82 },
                    { new Guid("90ec31c5-e5fa-4cef-adf9-ca2ad65896b0"), "Isidro Petkovic", 8, "Ergonomically designed pillow with breathable bamboo cover.", 62, "My Louisiana Sky", 62 },
                    { new Guid("94531835-1b96-4804-97a8-13cdb33be870"), "Jaynell Nutton", 10, "Light and crispy baked chips, a healthier snack option.", 34, "Business of Fancydancing, The", 34 },
                    { new Guid("99fa40dc-076c-4f54-931d-e7fe77385f41"), "Jasun Crapper", 11, "Quick and easy rice pilaf mix for side dishes.", 35, "Edge of Seventeen", 35 },
                    { new Guid("9c57b070-22f4-4d6f-97c4-3ac3a1bd6cc7"), "Maris Burgett", 2, "Heatstroke", 9, "Phantom, The", 9 },
                    { new Guid("9c961726-f1d0-41f3-8c42-e8aa36b2fc1c"), "Antwan Hewins", 2, "Dystrophy of retina", 4, "Grumpier Old Men", 4 },
                    { new Guid("9cfc33e9-fd14-4d4d-9bb9-4cfd1405a2b9"), "Santo Ashbrook", 3, "Salter-Harris Type III physeal fracture of lower end of radius", 9, "Other Side of the Bed, The (Lado de la cama, El)", 9 },
                    { new Guid("a2a29509-076a-49ca-8552-f65e1eb00384"), "Murray Dubery", 11, "Sweet mixture of cinnamon and sugar for sprinkling.", 32, "Weddings and Babies ", 32 },
                    { new Guid("a3794d6e-9532-4950-8965-8189868562ef"), "Amalea Sidden", 3, "Nutritious and hearty pasta made from whole wheat flour.", 56, "Personals, The (Zheng hun qi shi)", 56 },
                    { new Guid("a55f0c23-b173-4ce5-a67d-9d2630597e7e"), "Sharie Giauque", 1, "Gonococcal infection of lower genitourinary tract", 6, "Book of Life, The", 6 },
                    { new Guid("a97f2ee1-9f04-43de-bd5c-7a4df9ecaf86"), "Rosamond Daveran", 6, "Fun book filled with puzzles and games for kids.", 22, "Get Over It", 22 },
                    { new Guid("aaa3b385-032e-408f-8f9d-ae8e04d66596"), "Octavius Ibotson", 2, "Space-saving cup that folds flat for easy storage.", 72, "As Good as Dead", 72 },
                    { new Guid("ab5e31da-7799-4778-a4e0-62db9b98600a"), "Collie Monini", 6, "Flexible tray for easy-release ice cubes.", 55, "Code, The", 55 },
                    { new Guid("ac795052-aaf9-41c4-92f5-44056b425c5f"), "Eachelle Bruckman", 6, "A fashion-forward bomber jacket to elevate your casual looks.", 79, "Street Angel", 79 },
                    { new Guid("acb3b5d0-4f5b-49a8-8b7c-0f175cbf41a1"), "Terrence Petry", 9, "Sweet popcorn coated in a mixture of cinnamon and sugar.", 20, "Generation War", 20 },
                    { new Guid("af4ff5db-9c95-4904-8261-826a3cc4db0d"), "Blair Galvis", 1, "Separation of muscle (nontraumatic)", 3, "Free Zone", 3 },
                    { new Guid("af8a666f-0167-4d2b-8672-ff5acf49091a"), "Viviene Klimas", 2, "A fresh salad made with black beans, corn, and a zesty dressing, great for summer cookouts.", 21, "Mad Love (Sappho)", 21 },
                    { new Guid("b4f1278c-45f8-4950-a77e-e8cfbd993abf"), "Lauraine Coucher", 5, "Complete sculpting tools for artists.", 45, "Wholly Moses", 45 },
                    { new Guid("b5158d69-a32c-4f77-a15e-0deeb715a422"), "Frans Rames", 8, "Frozen shrimp seasoned with chili and lime, perfect for quick dinners.", 13, "Mother's Courage: Talking Back to Autism, A", 13 },
                    { new Guid("bb10f212-c412-4da4-9e01-54dff7217207"), "Judah Handlin", 8, "Set of reusable stainless steel straws for drinks.", 77, "Ju-on: The Grudge 2", 77 },
                    { new Guid("bb9b0f9d-990e-4a67-ab59-668b23d9a9a6"), "Leonid Patten", 5, "Trendy leggings with a unique graphic print, versatile for workouts and casual wear.", 40, "Model Couple, The (Le couple témoin)", 40 },
                    { new Guid("bc0f2ed8-171a-4155-ae4d-f33dd82727f0"), "Kaiser Greggs", 5, "Brews coffee with a smooth flavor without bitterness.", 71, "Three Seasons", 71 },
                    { new Guid("c024c7ef-b7f0-4fa0-abf8-ecc5f51e0cc0"), "Umberto Fagan", 4, "Rich and creamy tomato basil soup, perfect with a grilled cheese.", 38, "Girl Model", 38 },
                    { new Guid("c2a904cc-83c2-46a7-8466-7cebb500420b"), "Giffie Bonsale", 4, "Mix to create a delicious chia seed pudding in just a few minutes.", 29, "It's Good to Be Alive", 29 },
                    { new Guid("c5dfdaea-a696-40f0-8383-35892e5d396c"), "Yolanda Duncanson", 4, "Juicy and tender beef sirloin steak, grass-fed.", 19, "The Vengeance of Fu Manchu", 19 },
                    { new Guid("c7223325-3b1a-4175-964d-9ff61d7a485f"), "Elenore Bonnavant", 3, "A convenient meal kit for making a delicious beef taco skillet at home.", 64, "Bite the Bullet", 64 },
                    { new Guid("c8243167-8e15-4196-9ccd-b002874aca44"), "Janeen Durnford", 6, "Crisp and delicious organic apples.", 65, "Sarah Silverman:  We Are Miracles", 65 },
                    { new Guid("c8660f77-c183-4f8c-8f99-6e8b7c55cc06"), "Wilfred Paquin", 7, "Burn of unspecified degree", 1, "Moloch", 1 },
                    { new Guid("ca4db238-ca7e-447a-b169-37a6e3f33296"), "Hyacinthie Blizard", 4, "Qi-certified charger for fast wireless charging of smartphones.", 58, "The Story of Robin Hood and His Merrie Men", 58 },
                    { new Guid("cbc3779c-f158-4543-b6f3-6872576035fc"), "Gilda Labet", 6, "Anti-fog ski goggles for winter sports.", 12, "War on Democracy, The", 12 },
                    { new Guid("cd7ac5fd-0fc3-493d-8e97-d6f239b6897c"), "Stormy Latta", 5, "HEPA air purifier for clean indoor air.", 16, "Puppet Master", 16 },
                    { new Guid("d0069c60-1bb3-49f3-8307-bd5ed4a50544"), "Myrtis Reinhard", 1, "Chronic periodontitis", 5, "Salting the Battlefield", 5 },
                    { new Guid("d034954b-f61a-4926-b9c9-f3a4b0e4f626"), "Valentijn Charity", 11, "Crispy chicken nuggets for quick meals.", 28, "Aviator's Wife, The (La femme de l'aviateur)", 28 },
                    { new Guid("d0afd1e5-57b3-4694-9fba-b2b9db4f1474"), "Clyde Plak", 7, "All-in-one kit for making delicious chicken salad.", 1, "Séraphine", 1 },
                    { new Guid("d14feee2-e37d-471f-9eda-7ce87977fd78"), "Luigi Cash", 1, "Acute tonsillitis", 1, "Cast a Dark Shadow (Angel)", 1 },
                    { new Guid("d4ab2ecc-609a-4519-8dbb-eeb969939b5f"), "Hale Stuchburie", 9, "Soft and strong toilet paper for everyday use.", 4, "Aces: Iron Eagle III", 4 },
                    { new Guid("d66da54f-abc3-4094-9abc-d2bd304d3f3c"), "Laure Jugging", 9, "Stay hydrated with refreshing flavored water.", 67, "On the Edge (Hak bak do)", 67 },
                    { new Guid("d72a788f-dc57-492a-b62f-f268557dc96c"), "Dierdre Howey", 10, "Multi-functional grater for cheese and vegetables.", 68, "Shadow People", 68 },
                    { new Guid("d7874d05-2e51-4da4-9217-0caa7f107561"), "Inness Brenton", 9, "Reusable tote bags for shopping and eco-friendly living.", 42, "Room 237", 42 },
                    { new Guid("d81c58f8-c26f-4cb2-9ebf-0043cd6fbfdb"), "Benedikta Tinmouth", 11, "Ready-to-bake garlic breadsticks, perfect with pasta dishes.", 59, "Maniac", 59 },
                    { new Guid("da9302d2-2899-473e-aa12-89d8fa200901"), "Bink Smithies", 7, "Ready-to-eat salad with kale, lemon, and cheese.", 9, "Is the Man Who Is Tall Happy?", 9 },
                    { new Guid("e0cf2ddf-4568-49e8-b3ba-c717a89d03d1"), "Laura Rabjohn", 10, "Compact laundry bag for travel use.", 15, "Follow the Bitch", 15 },
                    { new Guid("e1ab9093-d731-46e7-b682-d8a524256153"), "Phedra Dumingos", 10, "A mix of grilled vegetables, ready to heat for quick side dishes.", 30, "Strange Cargo", 30 },
                    { new Guid("e4c7412e-dc26-4428-87f8-2f79bc69cdea"), "Jackie Pavitt", 4, "Tender riblets coated in a honey barbecue glaze, perfect for grilling or baking.", 5, "Suzhou River (Suzhou he)", 5 },
                    { new Guid("e5ffb3e2-35fb-4e65-b9e3-ee542a454c8b"), "Kora Rosenthaler", 7, "Freshly baked rustic bread, perfect for sandwiches or dipping in olive oil.", 37, "Beer League", 37 },
                    { new Guid("e67ad3cc-0049-4576-8e5f-58a5f0c49776"), "Jacquelin McAvinchey", 9, "Soft cotton pajama set for cozy nights in.", 39, "Yolki", 39 },
                    { new Guid("e685bbb4-9f8f-4eb1-9874-a4306773bb32"), "Syd MacAloren", 7, "Sweet peach halves packed in juice, great for desserts.", 53, "Resident Evil", 53 },
                    { new Guid("eaf263b6-cb6a-40b6-a490-81e37437f923"), "Devin Bramwell", 1, "Venous complication", 2, "Lego Star Wars: The Empire Strikes Out", 2 },
                    { new Guid("ee1e1fca-1536-4458-9699-56c4055418ee"), "Maureene Paragreen", 11, "Natural pink salt, perfect for seasoning.", 7, "Amen.", 7 },
                    { new Guid("ef366006-b902-479b-a54b-3b711c8c64fc"), "Goldia Siddens", 7, "Farm fresh eggs, essential for breakfast.", 78, "Iceman", 78 },
                    { new Guid("f1aae3fd-482c-4bf4-a940-e3e15780806f"), "Fredek Adamowitz", 9, "Elevate your outfit with this sophisticated velvet blazer.", 24, "Racing Stripes", 24 },
                    { new Guid("f34d64ed-786d-4035-8da5-436979306744"), "Crystie Delgadillo", 1, "Hypermobility of coccyx", 2, "Never Too Young to Die", 2 },
                    { new Guid("f4d51cd9-3980-40a4-a364-5a6dc4d59acf"), "Alleen de Najera", 9, "Professional-grade nail care set for manicures and pedicures.", 50, "Ballet Shoes", 50 },
                    { new Guid("f66730ac-1d29-4f4b-9718-0c5045426a09"), "Minne Baradel", 9, "Frozen pizza loaded with fresh vegetables and mozzarella cheese.", 27, "Necessary Roughness", 27 },
                    { new Guid("f7b64763-6d93-4ffd-a43e-96aa5e20a1d2"), "Ezmeralda O'Hone", 4, "Adjustable weighted jump rope for workouts.", 63, "Grudge 3, The", 63 },
                    { new Guid("fb6cfc75-fa54-4c30-8d56-91608969484c"), "Bunny Wapplington", 11, "Crispy chicken bites, perfect for dipping.", 26, "In Secret", 26 },
                    { new Guid("fc8be6bc-08f3-45d0-b38f-38828baf7985"), "Loydie Glewe", 11, "Stylish wine rack to store and display bottles.", 48, "Meet Me at the Fair", 48 },
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

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBorrowingRequestDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

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
