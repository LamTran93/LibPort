using LibPort.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LibPort.Contexts.EntityConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Description)
                .HasMaxLength(1000);

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.CategoryId)
                .IsRequired(false);

            builder.Property(b => b.Version)
                .IsRowVersion();

            builder.Property(b => b.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getutcdate()");
            builder.Property(b => b.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("getutcdate()");

            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Book
                {
                    Id = Guid.Parse("d14feee2-e37d-471f-9eda-7ce87977fd78"),
                    Title = "Cast a Dark Shadow (Angel)",
                    Description = "Acute tonsillitis",
                    Author = "Luigi Cash",
                    CategoryId = 1,
                    Quantity = 1,
                    Total = 1
                },
                new Book
                {
                    Id = Guid.Parse("f34d64ed-786d-4035-8da5-436979306744"),
                    Title = "Never Too Young to Die",
                    Description = "Hypermobility of coccyx",
                    Author = "Crystie Delgadillo",
                    CategoryId = 1,
                    Quantity = 2,
                    Total = 2
                },
                new Book
                {
                    Id = Guid.Parse("af4ff5db-9c95-4904-8261-826a3cc4db0d"),
                    Title = "Free Zone",
                    Description = "Separation of muscle (nontraumatic)",
                    Author = "Blair Galvis",
                    CategoryId = 1,
                    Quantity = 3,
                    Total = 3
                },
                new Book
                {
                    Id = Guid.Parse("636f189e-3c66-4b81-99c2-5efb858adf4e"),
                    Title = "Disaster Zone: Volcano in New York",
                    Description = "Dysplasia of vagina",
                    Author = "Jarrett Leasure",
                    CategoryId = 1,
                    Quantity = 4,
                    Total = 4
                },
                new Book
                {
                    Id = Guid.Parse("d0069c60-1bb3-49f3-8307-bd5ed4a50544"),
                    Title = "Salting the Battlefield",
                    Description = "Chronic periodontitis",
                    Author = "Myrtis Reinhard",
                    CategoryId = 1,
                    Quantity = 5,
                    Total = 5
                },
                new Book
                {
                    Id = Guid.Parse("a55f0c23-b173-4ce5-a67d-9d2630597e7e"),
                    Title = "Book of Life, The",
                    Description = "Gonococcal infection of lower genitourinary tract",
                    Author = "Sharie Giauque",
                    CategoryId = 1,
                    Quantity = 6,
                    Total = 6
                },
                new Book
                {
                    Id = Guid.Parse("15ae6a7e-93b5-4e60-8a3c-328c06f2094e"),
                    Title = "Bossa Nova",
                    Description = "Acute osteomyelitis",
                    Author = "Ramonita Hatzell",
                    CategoryId = 12,
                    Quantity = 7,
                    Total = 7
                },
                new Book
                {
                    Id = Guid.Parse("7b75b12d-3dc4-465a-ae54-bcd0fe2dc57a"),
                    Title = "Police Story",
                    Description = "Enthesopathy of hip region",
                    Author = "Eddy Marney",
                    CategoryId = 5,
                    Quantity = 8,
                    Total = 8
                },
                new Book
                {
                    Id = Guid.Parse("9cfc33e9-fd14-4d4d-9bb9-4cfd1405a2b9"),
                    Title = "Other Side of the Bed, The (Lado de la cama, El)",
                    Description = "Salter-Harris Type III physeal fracture of lower end of radius",
                    Author = "Santo Ashbrook",
                    CategoryId = 3,
                    Quantity = 9,
                    Total = 9
                },
                new Book
                {
                    Id = Guid.Parse("6a2ecb2f-ec0b-4e27-b6f8-f8f9842ce4a2"),
                    Title = "Inside Job",
                    Description = "Gout, unspecified",
                    Author = "Rebeca Mcsherry",
                    CategoryId = 4,
                    Quantity = 10,
                    Total = 10
                },
                new Book
                {
                    Id = Guid.Parse("c8660f77-c183-4f8c-8f99-6e8b7c55cc06"),
                    Title = "Moloch",
                    Description = "Burn of unspecified degree",
                    Author = "Wilfred Paquin",
                    CategoryId = 7,
                    Quantity = 1,
                    Total = 1
                },
                new Book
                {
                    Id = Guid.Parse("eaf263b6-cb6a-40b6-a490-81e37437f923"),
                    Title = "Lego Star Wars: The Empire Strikes Out",
                    Description = "Venous complication",
                    Author = "Devin Bramwell",
                    CategoryId = 1,
                    Quantity = 2,
                    Total = 2
                },
                new Book
                {
                    Id = Guid.Parse("0d60e77e-d9a6-4d47-84b2-7058b265b963"),
                    Title = "Man Called Peter, A",
                    Description = "Nontraumatic rupture of muscle",
                    Author = "Florencio Lackey",
                    CategoryId = 6,
                    Quantity = 3,
                    Total = 3
                },
                new Book
                {
                    Id = Guid.Parse("9c961726-f1d0-41f3-8c42-e8aa36b2fc1c"),
                    Title = "Grumpier Old Men",
                    Description = "Dystrophy of retina",
                    Author = "Antwan Hewins",
                    CategoryId = 2,
                    Quantity = 4,
                    Total = 4
                },
                new Book
                {
                    Id = Guid.Parse("12b4719a-14f7-490f-b5d5-19104de89ff9"),
                    Title = "Sunset Park",
                    Description = "Secondary malignant neoplasm of bone",
                    Author = "Lanny Florez",
                    CategoryId = 9,
                    Quantity = 5,
                    Total = 5
                },
                new Book
                {
                    Id = Guid.Parse("5083e315-fba5-4c27-8a24-6f574d3563ce"),
                    Title = "Rainbow Thief, The",
                    Description = "Anxiety disorder",
                    Author = "Laurence Dinsmore",
                    CategoryId = 11,
                    Quantity = 6,
                    Total = 6
                },
                new Book
                {
                    Id = Guid.Parse("14b35443-c5cc-44bb-9823-12ff1eaee847"),
                    Title = "Parallax View, The",
                    Description = "Intestinal volvulus",
                    Author = "Jaimee Gandara",
                    CategoryId = 4,
                    Quantity = 7,
                    Total = 7
                },
                new Book
                {
                    Id = Guid.Parse("fc994b85-01e0-4660-96f3-c7cf75b59e02"),
                    Title = "Joy Ride 2: Dead Ahead",
                    Description = "Preterm labor",
                    Author = "Cortney Mellen",
                    CategoryId = 1,
                    Quantity = 8,
                    Total = 8
                },
                new Book
                {
                    Id = Guid.Parse("9c57b070-22f4-4d6f-97c4-3ac3a1bd6cc7"),
                    Title = "Phantom, The",
                    Description = "Heatstroke",
                    Author = "Maris Burgett",
                    CategoryId = 2,
                    Quantity = 9,
                    Total = 9
                },
                new Book
                {
                    Id = Guid.Parse("2abbdca7-df70-498c-97ed-2c78d1a24fcb"),
                    Title = "Samantha: An American Girl Holiday",
                    Description = "Postoperative infection",
                    Author = "Tami Daignault",
                    CategoryId = 7,
                    Quantity = 10,
                    Total = 10
                },
                new Book
                {
                    Id = Guid.Parse("d0afd1e5-57b3-4694-9fba-b2b9db4f1474"),
                    Title = "Séraphine",
                    Description = "All-in-one kit for making delicious chicken salad.",
                    Author = "Clyde Plak",
                    CategoryId = 7,
                    Quantity = 1,
                    Total = 1
                },
                new Book
                {
                    Id = Guid.Parse("23d06134-f7b7-498f-9140-7226f97429de"),
                    Title = "How Do You Know",
                    Description = "Reusable silicone lids for covering food in bowls and storage containers.",
                    Author = "Lulita Littlepage",
                    CategoryId = 11,
                    Quantity = 2,
                    Total = 2
                },
                new Book
                {
                    Id = Guid.Parse("81403f89-497f-4195-8fdb-1a563a26a749"),
                    Title = "Maria's Lovers",
                    Description = "Deliciously smoked salmon, great for bagels.",
                    Author = "Josh Ivanenko",
                    CategoryId = 6,
                    Quantity = 3,
                    Total = 3
                },
                new Book
                {
                    Id = Guid.Parse("d4ab2ecc-609a-4519-8dbb-eeb969939b5f"),
                    Title = "Aces: Iron Eagle III",
                    Description = "Soft and strong toilet paper for everyday use.",
                    Author = "Hale Stuchburie",
                    CategoryId = 9,
                    Quantity = 4,
                    Total = 4
                },
                new Book
                {
                    Id = Guid.Parse("e4c7412e-dc26-4428-87f8-2f79bc69cdea"),
                    Title = "Suzhou River (Suzhou he)",
                    Description = "Tender riblets coated in a honey barbecue glaze, perfect for grilling or baking.",
                    Author = "Jackie Pavitt",
                    CategoryId = 4,
                    Quantity = 5,
                    Total = 5
                },
                new Book
                {
                    Id = Guid.Parse("4eb50b68-0697-414b-a8b5-d96132d536cf"),
                    Title = "Bullitt",
                    Description = "Professional sharpening system for kitchen knives.",
                    Author = "Leslie Gloy",
                    CategoryId = 8,
                    Quantity = 6,
                    Total = 6
                },
                new Book
                {
                    Id = Guid.Parse("ee1e1fca-1536-4458-9699-56c4055418ee"),
                    Title = "Amen.",
                    Description = "Natural pink salt, perfect for seasoning.",
                    Author = "Maureene Paragreen",
                    CategoryId = 11,
                    Quantity = 7,
                    Total = 7
                },
                new Book
                {
                    Id = Guid.Parse("05b596de-000f-4640-868a-f7ce351fca72"),
                    Title = "Klip (Clip)",
                    Description = "Gluten-free bread mix made with almond flour.",
                    Author = "Georgianne Seeds",
                    CategoryId = 8,
                    Quantity = 8,
                    Total = 8
                },
                new Book
                {
                    Id = Guid.Parse("da9302d2-2899-473e-aa12-89d8fa200901"),
                    Title = "Is the Man Who Is Tall Happy?",
                    Description = "Ready-to-eat salad with kale, lemon, and cheese.",
                    Author = "Bink Smithies",
                    CategoryId = 7,
                    Quantity = 9,
                    Total = 9
                },
                new Book
                {
                    Id = Guid.Parse("13622935-03ef-471d-967a-425b7ee8ade6"),
                    Title = "New Rose Hotel",
                    Description = "Small toolkit with essentials for home repairs.",
                    Author = "Idalia Erskin",
                    CategoryId = 2,
                    Quantity = 10,
                    Total = 10
                },
                new Book
                {
                    Id = Guid.Parse("02809c89-bd9f-4248-9037-c7dac59bce94"),
                    Title = "The Borderlands",
                    Description = "Hands-free waist pack for carrying essentials while walking your dog.",
                    Author = "Jemimah Wilsey",
                    CategoryId = 3,
                    Quantity = 11,
                    Total = 11
                },
                new Book
                {
                    Id = Guid.Parse("cbc3779c-f158-4543-b6f3-6872576035fc"),
                    Title = "War on Democracy, The",
                    Description = "Anti-fog ski goggles for winter sports.",
                    Author = "Gilda Labet",
                    CategoryId = 6,
                    Quantity = 12,
                    Total = 12
                },
                new Book
                {
                    Id = Guid.Parse("b5158d69-a32c-4f77-a15e-0deeb715a422"),
                    Title = "Mother's Courage: Talking Back to Autism, A",
                    Description = "Frozen shrimp seasoned with chili and lime, perfect for quick dinners.",
                    Author = "Frans Rames",
                    CategoryId = 8,
                    Quantity = 13,
                    Total = 13
                },
                new Book
                {
                    Id = Guid.Parse("1aac33d9-836e-4217-8a80-6d3adb072d71"),
                    Title = "Persepolis",
                    Description = "Oven-roasted potatoes tossed in garlic and parmesan cheese seasoning.",
                    Author = "Fionnula Readshall",
                    CategoryId = 4,
                    Quantity = 14,
                    Total = 14
                },
                new Book
                {
                    Id = Guid.Parse("e0cf2ddf-4568-49e8-b3ba-c717a89d03d1"),
                    Title = "Follow the Bitch",
                    Description = "Compact laundry bag for travel use.",
                    Author = "Laura Rabjohn",
                    CategoryId = 10,
                    Quantity = 15,
                    Total = 15
                },
                new Book
                {
                    Id = Guid.Parse("cd7ac5fd-0fc3-493d-8e97-d6f239b6897c"),
                    Title = "Puppet Master",
                    Description = "HEPA air purifier for clean indoor air.",
                    Author = "Stormy Latta",
                    CategoryId = 5,
                    Quantity = 16,
                    Total = 16
                },
                new Book
                {
                    Id = Guid.Parse("00d358c7-ab98-4273-b4c3-3c672cec527d"),
                    Title = "Meatballs III",
                    Description = "Compact and portable backpack for day trips and travel.",
                    Author = "Lolita Worpole",
                    CategoryId = 5,
                    Quantity = 17,
                    Total = 17
                }, new Book
                {
                    Id = Guid.Parse("3036a334-da9b-444e-b56b-45df1fbfa7b4"),
                    Title = "Righteous Kill",
                    Description = "Infuser for brewing loose-leaf herbal teas easily.",
                    Author = "Gerrard Abramsky",
                    CategoryId = 8,
                    Quantity = 18,
                    Total = 18
                },
                new Book
                {
                    Id = Guid.Parse("c5dfdaea-a696-40f0-8383-35892e5d396c"),
                    Title = "The Vengeance of Fu Manchu",
                    Description = "Juicy and tender beef sirloin steak, grass-fed.",
                    Author = "Yolanda Duncanson",
                    CategoryId = 4,
                    Quantity = 19,
                    Total = 19
                },
                new Book
                {
                    Id = Guid.Parse("acb3b5d0-4f5b-49a8-8b7c-0f175cbf41a1"),
                    Title = "Generation War",
                    Description = "Sweet popcorn coated in a mixture of cinnamon and sugar.",
                    Author = "Terrence Petry",
                    CategoryId = 9,
                    Quantity = 20,
                    Total = 20
                },
                new Book
                {
                    Id = Guid.Parse("af8a666f-0167-4d2b-8672-ff5acf49091a"),
                    Title = "Mad Love (Sappho)",
                    Description = "A fresh salad made with black beans, corn, and a zesty dressing, great for summer cookouts.",
                    Author = "Viviene Klimas",
                    CategoryId = 2,
                    Quantity = 21,
                    Total = 21
                },
                new Book
                {
                    Id = Guid.Parse("a97f2ee1-9f04-43de-bd5c-7a4df9ecaf86"),
                    Title = "Get Over It",
                    Description = "Fun book filled with puzzles and games for kids.",
                    Author = "Rosamond Daveran",
                    CategoryId = 6,
                    Quantity = 22,
                    Total = 22
                },
                new Book
                {
                    Id = Guid.Parse("562ea69d-8995-45be-9d07-d1149919e526"),
                    Title = "Escape from Dartmoor",
                    Description = "Rechargeable LED camping lantern for outdoor use.",
                    Author = "Freddy Lacroix",
                    CategoryId = 3,
                    Quantity = 23,
                    Total = 23
                },
                new Book
                {
                    Id = Guid.Parse("f1aae3fd-482c-4bf4-a940-e3e15780806f"),
                    Title = "Racing Stripes",
                    Description = "Elevate your outfit with this sophisticated velvet blazer.",
                    Author = "Fredek Adamowitz",
                    CategoryId = 9,
                    Quantity = 24,
                    Total = 24
                },
                new Book
                {
                    Id = Guid.Parse("8d2c9032-539f-4f63-959b-feec2204ae57"),
                    Title = "Operation Homecoming: Writing the Wartime Experience",
                    Description = "Fresh and zesty salsa, perfect for nachos.",
                    Author = "Nicola Decayette",
                    CategoryId = 11,
                    Quantity = 25,
                    Total = 25
                },
                new Book
                {
                    Id = Guid.Parse("fb6cfc75-fa54-4c30-8d56-91608969484c"),
                    Title = "In Secret",
                    Description = "Crispy chicken bites, perfect for dipping.",
                    Author = "Bunny Wapplington",
                    CategoryId = 11,
                    Quantity = 26,
                    Total = 26
                },
                new Book
                {
                    Id = Guid.Parse("f66730ac-1d29-4f4b-9718-0c5045426a09"),
                    Title = "Necessary Roughness",
                    Description = "Frozen pizza loaded with fresh vegetables and mozzarella cheese.",
                    Author = "Minne Baradel",
                    CategoryId = 9,
                    Quantity = 27,
                    Total = 27
                },
                new Book
                {
                    Id = Guid.Parse("d034954b-f61a-4926-b9c9-f3a4b0e4f626"),
                    Title = "Aviator's Wife, The (La femme de l'aviateur)",
                    Description = "Crispy chicken nuggets for quick meals.",
                    Author = "Valentijn Charity",
                    CategoryId = 11,
                    Quantity = 28,
                    Total = 28
                },
                new Book
                {
                    Id = Guid.Parse("c2a904cc-83c2-46a7-8466-7cebb500420b"),
                    Title = "It's Good to Be Alive",
                    Description = "Mix to create a delicious chia seed pudding in just a few minutes.",
                    Author = "Giffie Bonsale",
                    CategoryId = 4,
                    Quantity = 29,
                    Total = 29
                },
                new Book
                {
                    Id = Guid.Parse("e1ab9093-d731-46e7-b682-d8a524256153"),
                    Title = "Strange Cargo",
                    Description = "A mix of grilled vegetables, ready to heat for quick side dishes.",
                    Author = "Phedra Dumingos",
                    CategoryId = 10,
                    Quantity = 30,
                    Total = 30
                },
                new Book
                {
                    Id = Guid.Parse("2e027286-404b-4030-adb5-32b0b1627207"),
                    Title = "Viva Las Vegas",
                    Description = "Compact organizer for cosmetics during travel.",
                    Author = "Doralia Knowling",
                    CategoryId = 9,
                    Quantity = 31,
                    Total = 31
                },
                new Book
                {
                    Id = Guid.Parse("a2a29509-076a-49ca-8552-f65e1eb00384"),
                    Title = "Weddings and Babies ",
                    Description = "Sweet mixture of cinnamon and sugar for sprinkling.",
                    Author = "Murray Dubery",
                    CategoryId = 11,
                    Quantity = 32,
                    Total = 32
                },
                new Book
                {
                    Id = Guid.Parse("8e15def4-c718-4c7d-bd97-82873a02d398"),
                    Title = "Faust",
                    Description = "Frozen mix of colorful stir-fry veggies.",
                    Author = "Cecilius Rein",
                    CategoryId = 9,
                    Quantity = 33,
                    Total = 33
                },
                new Book
                {
                    Id = Guid.Parse("94531835-1b96-4804-97a8-13cdb33be870"),
                    Title = "Business of Fancydancing, The",
                    Description = "Light and crispy baked chips, a healthier snack option.",
                    Author = "Jaynell Nutton",
                    CategoryId = 10,
                    Quantity = 34,
                    Total = 34
                },
                new Book
                {
                    Id = Guid.Parse("99fa40dc-076c-4f54-931d-e7fe77385f41"),
                    Title = "Edge of Seventeen",
                    Description = "Quick and easy rice pilaf mix for side dishes.",
                    Author = "Jasun Crapper",
                    CategoryId = 11,
                    Quantity = 35,
                    Total = 35
                },
                new Book
                {
                    Id = Guid.Parse("58fc28e6-4a55-4d1b-b54f-739bef68f838"),
                    Title = "Pépé le Moko",
                    Description = "Crispy and crunchy snack made from assorted vegetables.",
                    Author = "Clay Loker",
                    CategoryId = 10,
                    Quantity = 36,
                    Total = 36
                },
                new Book
                {
                    Id = Guid.Parse("e5ffb3e2-35fb-4e65-b9e3-ee542a454c8b"),
                    Title = "Beer League",
                    Description = "Freshly baked rustic bread, perfect for sandwiches or dipping in olive oil.",
                    Author = "Kora Rosenthaler",
                    CategoryId = 7,
                    Quantity = 37,
                    Total = 37
                },
                new Book
                {
                    Id = Guid.Parse("c024c7ef-b7f0-4fa0-abf8-ecc5f51e0cc0"),
                    Title = "Girl Model",
                    Description = "Rich and creamy tomato basil soup, perfect with a grilled cheese.",
                    Author = "Umberto Fagan",
                    CategoryId = 4,
                    Quantity = 38,
                    Total = 38
                },
                new Book
                {
                    Id = Guid.Parse("e67ad3cc-0049-4576-8e5f-58a5f0c49776"),
                    Title = "Yolki",
                    Description = "Soft cotton pajama set for cozy nights in.",
                    Author = "Jacquelin McAvinchey",
                    CategoryId = 9,
                    Quantity = 39,
                    Total = 39
                },
                new Book
                {
                    Id = Guid.Parse("bb9b0f9d-990e-4a67-ab59-668b23d9a9a6"),
                    Title = "Model Couple, The (Le couple témoin)",
                    Description = "Trendy leggings with a unique graphic print, versatile for workouts and casual wear.",
                    Author = "Leonid Patten",
                    CategoryId = 5,
                    Quantity = 40,
                    Total = 40
                },
                new Book
                {
                    Id = Guid.Parse("8873bb31-91d2-4fb9-befc-ee186bb78d23"),
                    Title = "Adventure of Sherlock Holmes' Smarter Brother, The",
                    Description = "Lightweight hair dryer with multiple speed settings.",
                    Author = "Annalee Oakenfield",
                    CategoryId = 3,
                    Quantity = 41,
                    Total = 41
                },
                new Book
                {
                    Id = Guid.Parse("d7874d05-2e51-4da4-9217-0caa7f107561"),
                    Title = "Room 237",
                    Description = "Reusable tote bags for shopping and eco-friendly living.",
                    Author = "Inness Brenton",
                    CategoryId = 9,
                    Quantity = 42,
                    Total = 42
                },
                new Book
                {
                    Id = Guid.Parse("5719b541-9fe4-4230-8491-b2bac997f22c"),
                    Title = "Angst",
                    Description = "Programmable pet feeder for scheduled meals.",
                    Author = "Georgine Pedel",
                    CategoryId = 8,
                    Quantity = 43,
                    Total = 43
                },
                new Book
                {
                    Id = Guid.Parse("402a2e5e-1186-4c37-9ae6-f448e3f06a40"),
                    Title = "Backwood Philosopher (Havukka-ahon ajattelija)",
                    Description = "Crispy spring rolls filled with vegetables",
                    Author = "Ingaborg Limb",
                    CategoryId = 11,
                    Quantity = 44,
                    Total = 44
                },
                new Book
                {
                    Id = Guid.Parse("b4f1278c-45f8-4950-a77e-e8cfbd993abf"),
                    Title = "Wholly Moses",
                    Description = "Complete sculpting tools for artists.",
                    Author = "Lauraine Coucher",
                    CategoryId = 5,
                    Quantity = 45,
                    Total = 45
                },
                new Book
                {
                    Id = Guid.Parse("60eefe1a-0cab-4cdf-b15c-9d96d142d921"),
                    Title = "American Psycho",
                    Description = "Advanced electric toothbrush for effective cleaning.",
                    Author = "Timoteo Jeannel",
                    CategoryId = 7,
                    Quantity = 46,
                    Total = 46
                },
                new Book
                {
                    Id = Guid.Parse("4e06bbd1-6b56-410b-a325-f96ecf5f0f8a"),
                    Title = "Murder in the First",
                    Description = "A pack of assorted nut and protein bars for a quick energy boost.",
                    Author = "Clarance Omand",
                    CategoryId = 6,
                    Quantity = 47,
                    Total = 47
                },
                new Book
                {
                    Id = Guid.Parse("fc8be6bc-08f3-45d0-b38f-38828baf7985"),
                    Title = "Meet Me at the Fair",
                    Description = "Stylish wine rack to store and display bottles.",
                    Author = "Loydie Glewe",
                    CategoryId = 11,
                    Quantity = 48,
                    Total = 48
                },
                new Book
                {
                    Id = Guid.Parse("3af945dc-3b18-4d41-a856-eb92fa59eb80"),
                    Title = "Hell Is for Heroes",
                    Description = "Safety vest for pets during nighttime walks.",
                    Author = "Lucila Tilburn",
                    CategoryId = 4,
                    Quantity = 49,
                    Total = 49
                },
                new Book
                {
                    Id = Guid.Parse("f4d51cd9-3980-40a4-a364-5a6dc4d59acf"),
                    Title = "Ballet Shoes",
                    Description = "Professional-grade nail care set for manicures and pedicures.",
                    Author = "Alleen de Najera",
                    CategoryId = 9,
                    Quantity = 50,
                    Total = 50
                },
                new Book
                {
                    Id = Guid.Parse("8818a00c-cbe3-4f4b-a106-7b9455b63860"),
                    Title = "Time and Tide (Seunlau Ngaklau)",
                    Description = "Stay dry with these stylish waterproof rain boots.",
                    Author = "Nadeen Kincaid",
                    CategoryId = 6,
                    Quantity = 51,
                    Total = 51
                },
                new Book
                {
                    Id = Guid.Parse("87cc034c-af7a-4aa0-81c1-24077e4847c1"),
                    Title = "Gold",
                    Description = "Concentrated tomato paste, great for sauces.",
                    Author = "Nelia Kender",
                    CategoryId = 9,
                    Quantity = 52,
                    Total = 52
                },
                new Book
                {
                    Id = Guid.Parse("e685bbb4-9f8f-4eb1-9874-a4306773bb32"),
                    Title = "Resident Evil",
                    Description = "Sweet peach halves packed in juice, great for desserts.",
                    Author = "Syd MacAloren",
                    CategoryId = 7,
                    Quantity = 53,
                    Total = 53
                },
                new Book
                {
                    Id = Guid.Parse("1fcb574e-673f-4673-9bb7-2a57f630b220"),
                    Title = "New Daughter, The",
                    Description = "Creamy tahini made from ground sesame seeds.",
                    Author = "Esme Harsant",
                    CategoryId = 2,
                    Quantity = 54,
                    Total = 54
                }, new Book
                {
                    Id = Guid.Parse("ab5e31da-7799-4778-a4e0-62db9b98600a"),
                    Title = "Code, The",
                    Description = "Flexible tray for easy-release ice cubes.",
                    Author = "Collie Monini",
                    CategoryId = 6,
                    Quantity = 55,
                    Total = 55
                },
                new Book
                {
                    Id = Guid.Parse("a3794d6e-9532-4950-8965-8189868562ef"),
                    Title = "Personals, The (Zheng hun qi shi)",
                    Description = "Nutritious and hearty pasta made from whole wheat flour.",
                    Author = "Amalea Sidden",
                    CategoryId = 3,
                    Quantity = 56,
                    Total = 56
                },
                new Book
                {
                    Id = Guid.Parse("2aa246b3-7e77-4e9c-bf1e-4e948a741000"),
                    Title = "Small Town, The (a.k.a. The Town) (Kasaba)",
                    Description = "Convenient charging stand for smartphones and devices.",
                    Author = "Herc McGaffey",
                    CategoryId = 8,
                    Quantity = 57,
                    Total = 57
                },
                new Book
                {
                    Id = Guid.Parse("ca4db238-ca7e-447a-b169-37a6e3f33296"),
                    Title = "The Story of Robin Hood and His Merrie Men",
                    Description = "Qi-certified charger for fast wireless charging of smartphones.",
                    Author = "Hyacinthie Blizard",
                    CategoryId = 4,
                    Quantity = 58,
                    Total = 58
                },
                new Book
                {
                    Id = Guid.Parse("d81c58f8-c26f-4cb2-9ebf-0043cd6fbfdb"),
                    Title = "Maniac",
                    Description = "Ready-to-bake garlic breadsticks, perfect with pasta dishes.",
                    Author = "Benedikta Tinmouth",
                    CategoryId = 11,
                    Quantity = 59,
                    Total = 59
                }, new Book
                {
                    Id = Guid.Parse("170f7afa-c2df-42df-b4a9-4f522d4ccab8"),
                    Title = "Bad Company",
                    Description = "Wi-Fi camera with motion detection for home security.",
                    Author = "Beverley Lambal",
                    CategoryId = 11,
                    Quantity = 60,
                    Total = 60
                },
                new Book
                {
                    Id = Guid.Parse("677485a2-2823-4d97-a72b-d547f5db552e"),
                    Title = "Flirtation Walk",
                    Description = "Rechargeable LED lantern ideal for camping and outdoor activities.",
                    Author = "Josee Iacovucci",
                    CategoryId = 6,
                    Quantity = 61,
                    Total = 61
                },
                new Book
                {
                    Id = Guid.Parse("90ec31c5-e5fa-4cef-adf9-ca2ad65896b0"),
                    Title = "My Louisiana Sky",
                    Description = "Ergonomically designed pillow with breathable bamboo cover.",
                    Author = "Isidro Petkovic",
                    CategoryId = 8,
                    Quantity = 62,
                    Total = 62
                },
                new Book
                {
                    Id = Guid.Parse("f7b64763-6d93-4ffd-a43e-96aa5e20a1d2"),
                    Title = "Grudge 3, The",
                    Description = "Adjustable weighted jump rope for workouts.",
                    Author = "Ezmeralda O'Hone",
                    CategoryId = 4,
                    Quantity = 63,
                    Total = 63
                }, new Book
                {
                    Id = Guid.Parse("c7223325-3b1a-4175-964d-9ff61d7a485f"),
                    Title = "Bite the Bullet",
                    Description = "A convenient meal kit for making a delicious beef taco skillet at home.",
                    Author = "Elenore Bonnavant",
                    CategoryId = 3,
                    Quantity = 64,
                    Total = 64
                }, new Book
                {
                    Id = Guid.Parse("c8243167-8e15-4196-9ccd-b002874aca44"),
                    Title = "Sarah Silverman:  We Are Miracles",
                    Description = "Crisp and delicious organic apples.",
                    Author = "Janeen Durnford",
                    CategoryId = 6,
                    Quantity = 65,
                    Total = 65
                },
                new Book
                {
                    Id = Guid.Parse("4029dd19-9027-4b8d-9646-07547c162193"),
                    Title = "Bounce: Behind the Velvet Rope",
                    Description = "Moist brownie topped with sea salt and caramel drizzle.",
                    Author = "Francene Marriot",
                    CategoryId = 5,
                    Quantity = 66,
                    Total = 66
                },
                new Book
                {
                    Id = Guid.Parse("d66da54f-abc3-4094-9abc-d2bd304d3f3c"),
                    Title = "On the Edge (Hak bak do)",
                    Description = "Stay hydrated with refreshing flavored water.",
                    Author = "Laure Jugging",
                    CategoryId = 9,
                    Quantity = 67,
                    Total = 67
                },
                new Book
                {
                    Id = Guid.Parse("d72a788f-dc57-492a-b62f-f268557dc96c"),
                    Title = "Shadow People",
                    Description = "Multi-functional grater for cheese and vegetables.",
                    Author = "Dierdre Howey",
                    CategoryId = 10,
                    Quantity = 68,
                    Total = 68
                },
                new Book
                {
                    Id = Guid.Parse("51e3d063-2c80-416e-b4ae-836a3b4ab56f"),
                    Title = "Direct Contact",
                    Description = "Gentle brush for removing loose fur from pets.",
                    Author = "Justus Farran",
                    CategoryId = 5,
                    Quantity = 69,
                    Total = 69
                },
                new Book
                {
                    Id = Guid.Parse("8983ec49-ef9f-419d-88f0-fea619bc1bd2"),
                    Title = "Taxi to the Dark Side",
                    Description = "Comprehensive first aid kit for home or travel emergencies.",
                    Author = "Brande Pellew",
                    CategoryId = 4,
                    Quantity = 70,
                    Total = 70
                },
                new Book
                {
                    Id = Guid.Parse("bc0f2ed8-171a-4155-ae4d-f33dd82727f0"),
                    Title = "Three Seasons",
                    Description = "Brews coffee with a smooth flavor without bitterness.",
                    Author = "Kaiser Greggs",
                    CategoryId = 5,
                    Quantity = 71,
                    Total = 71
                },
                new Book
                {
                    Id = Guid.Parse("aaa3b385-032e-408f-8f9d-ae8e04d66596"),
                    Title = "As Good as Dead",
                    Description = "Space-saving cup that folds flat for easy storage.",
                    Author = "Octavius Ibotson",
                    CategoryId = 2,
                    Quantity = 72,
                    Total = 72
                },
                new Book
                {
                    Id = Guid.Parse("1ae97f4c-1003-4923-bd8b-86230e819310"),
                    Title = "Beautiful Girls",
                    Description = "Frozen zucchini slices coated in parmesan cheese, perfect for baking or frying.",
                    Author = "Heindrick Walcot",
                    CategoryId = 4,
                    Quantity = 73,
                    Total = 73
                },
                new Book
                {
                    Id = Guid.Parse("472d00c2-c68e-4003-a266-4cf0bc1d6df9"),
                    Title = "Kim",
                    Description = "Frozen sausage patties made with herbs and spices, perfect for breakfast sandwiches.",
                    Author = "Rudyard Egdal",
                    CategoryId = 9,
                    Quantity = 74,
                    Total = 74
                },
                new Book
                {
                    Id = Guid.Parse("80c888ba-4de4-4b69-a870-d97a2c05221c"),
                    Title = "Last Metro, The (Dernier métro, Le)",
                    Description = "Crunchy pretzels dipped in rich dark chocolate.",
                    Author = "Celia Robjohns",
                    CategoryId = 2,
                    Quantity = 75,
                    Total = 75
                },
                new Book
                {
                    Id = Guid.Parse("4640b125-c365-45e4-a8eb-2f4121aadd9e"),
                    Title = "Men with Guns",
                    Description = "Crunchy seeds perfect for toppings and baking.",
                    Author = "Irita Lomasny",
                    CategoryId = 2,
                    Quantity = 76,
                    Total = 76
                },
                new Book
                {
                    Id = Guid.Parse("bb10f212-c412-4da4-9e01-54dff7217207"),
                    Title = "Ju-on: The Grudge 2",
                    Description = "Set of reusable stainless steel straws for drinks.",
                    Author = "Judah Handlin",
                    CategoryId = 8,
                    Quantity = 77,
                    Total = 77
                },
                new Book
                {
                    Id = Guid.Parse("ef366006-b902-479b-a54b-3b711c8c64fc"),
                    Title = "Iceman",
                    Description = "Farm fresh eggs, essential for breakfast.",
                    Author = "Goldia Siddens",
                    CategoryId = 7,
                    Quantity = 78,
                    Total = 78
                },
                new Book
                {
                    Id = Guid.Parse("ac795052-aaf9-41c4-92f5-44056b425c5f"),
                    Title = "Street Angel",
                    Description = "A fashion-forward bomber jacket to elevate your casual looks.",
                    Author = "Eachelle Bruckman",
                    CategoryId = 6,
                    Quantity = 79,
                    Total = 79
                },
                new Book
                {
                    Id = Guid.Parse("873878ae-4224-4737-aca6-576ea7a37c2a"),
                    Title = "The Oscar",
                    Description = "Spreadable cream cheese with garlic and herbs.",
                    Author = "Dan Effemy",
                    CategoryId = 8,
                    Quantity = 80,
                    Total = 80
                },
                new Book
                {
                    Id = Guid.Parse("0053ea56-f57f-4bdf-8cae-e55cdcaf843f"),
                    Title = "Celluloid Closet, The",
                    Description = "Soft and warm corn tortillas, perfect for tacos and burritos.",
                    Author = "Christean Baldree",
                    CategoryId = 6,
                    Quantity = 81,
                    Total = 81
                },
                new Book
                {
                    Id = Guid.Parse("8eb071ad-37b3-41db-a45d-9adbd7c46f29"),
                    Title = "Little Girl Who Lives Down the Lane, The",
                    Description = "Hands-on experience with science and engineering projects.",
                    Author = "Kamilah Easey",
                    CategoryId = 9,
                    Quantity = 82,
                    Total = 82
                }
            );
        }
    }
}
