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
                .OnDelete(DeleteBehavior.NoAction);

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
                }
            );
        }
    }
}
