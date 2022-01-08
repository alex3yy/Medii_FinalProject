using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemShopModel.Data;
using ItemShopModel.Models;

namespace Zaharia_Alexandru_Lab2.Data {

    public class DbInitializer {

        public static void Initialize(ItemShopContext context)
        {
            context.Database.EnsureCreated();

            if (context.Items.Any()) {
                return; // BD a fost creata anterior
            }

            var manufacturers = new Manufacturer[]
            {
                new Manufacturer
                {
                    Name = "GIGABYTE TECHNOLOGY CO., LTD.",
                    Address = "No.6, Baoqiang Rd., Xindian Dist., New Taipei City 231, Taiwan",
                    PhoneNumber = "+886-2-8912-4000",
                    EmailAddress = "contact@gigabyte.com"
                },
                new Manufacturer
                {
                    Name = "ASUSTeK Computer Inc.",
                    Address = "Str. Calea Domneasca Nr. 345, Targoviste, Dambovita, Romania",
                    PhoneNumber = "0372.030.477",
                    EmailAddress = "contact@asusromania.ro"
                },
                new Manufacturer
                {
                    Name = "SAMSUNG Electronics",
                    Address = "SOSEAUA BUCURESTI-PLOIESTI 172-176 Cladire: A, Etaj: 5, BUCURESTI, SECTOR 1, Romania",
                    PhoneNumber = "0800872678",
                    EmailAddress = "contact@samsung.ro"
                },
                new Manufacturer
                {
                    Name = "Apple Inc.",
                    Address = "One Apple Park Way, Cupertino, CA",
                    PhoneNumber = "800–692–7753",
                    EmailAddress = "contact@apple.ro"
                }
            };

            foreach (Manufacturer manufacturer in manufacturers)
            {
                context.Manufacturers.Add(manufacturer);
            }

            context.SaveChanges();

            var items = new Item[]
            {
                new Item
                {
                    Title = "Telefon mobil Apple iPhone 13, 128GB, 5G, Midnight",
                    Description = "Intra in ecosistemul Apple si cumpara cel mai recent dispozitiv mobil cu noul procesor A15 Bionic.",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("Apple")).ID,
                    Price = Decimal.Parse("5300")
                },
                new Item
                {
                    Title = "Laptop Apple MacBook Air 13-inch, True Tone, procesor Apple M1, 8 nuclee CPU si 7 nuclee GPU, 8GB, 256GB, Space Grey, INT KB",
                    Description = "Cel mai subtire si mai usor notebook-ul nostru, complet transformat de cipul Apple M1. Viteza procesorului de pana la de 3,5 ori mai rapida. Viteza GPU-ului de pana la 5 ori mai rapida. Cel mai avansat motor neuronal pentru invatare automata de pana la de 9 ori mai rapida. Cea mai mare autonomie a bateriei vreodata pe un MacBook Air. Si un design silentios, fanless. Atata putere nu a fost niciodata atat de pregatita.",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("Apple")).ID,
                    Price = Decimal.Parse("6000")
                },
                new Item
                {
                    Title = "Placa Video GIGABYTE AORUS GeForce RTX 3070 MASTER 8G",
                    Description = "Activeaza puterea ray-traycing-ului prin noua serie 3000 si sparge bariera realitatii!",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("GIGABYTE")).ID,
                    Price = Decimal.Parse("7409")
                },
                new Item
                {
                    Title = "Laptop Gaming ASUS ROG Zephyrus G14 GA401QEC cu procesor AMD Ryzen™ 9 5900HS, 14\", WQHD, 120Hz, 16GB, 1TB SSD, NVIDIA® GeForce RTX™ 3050 Ti, Windows 10 Home, Alan Walker edition, Grey, ROG Remix",
                    Description = "Inspirat de una dintre culorile temei Alan Walker. G14 AlanWalker ofera un contrast de culoare la moda intre negru si albastru, care este exclusiv pentru G14 AlanWalker.",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("ASUSTeK")).ID,
                    Price = Decimal.Parse("6600")
                },
                new Item
                {
                    Title = "Televizor Samsung 55QN90A, 138 cm, Smart, 4K Ultra HD, Neo QLED, Clasa F",
                    Description = "Evolutia televizoarelor prin Neo QLED vine cu tehnologia Quantum Matrix, care controleaza cu precizie noul nostru Quantum Mini LED. Cu un control precis al luminii, te poti bucura de detalii deosebite, atat in cele mai intunecate scene, cat si in cele mai stralucitoare.",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("SAMSUNG")).ID,
                    Price = Decimal.Parse("6000")
                },
                new Item
                {
                    Title = "Cooler Procesor Gigabyte AORUS WATERFORCE X 280 ARGB, compatibil AMD/Intel",
                    Description = "Prin teste extinse, AORUS a dezvoltat cel mai sinergic design de palete de ventilator de 140 mm pentru a merge cu radiatorul de 280 mm. Acesta va oferi cea mai eficienta disipare a caldurii, mentinand in acelasi timp cele mai scazute niveluri de zgomot, chiar si la viteze maxime.",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("GIGABYTE")).ID,
                    Price = Decimal.Parse("967")
                },
                new Item
                {
                    Title = "Placa de baza GIGABYTE X570 AORUS MASTER Socket Am4",
                    Description = "In noaptea intunecata, fara stele, soimul loveste frica in inima pradei sale. Chiar si cu o vizibilitate minima, soimul isi gaseste prada si anticipeaza cu rabdare momentul perfect pentru a se opri pentru omor. Falconul cu privirea ascutita cu laser domina intunericul noptii in acelasi mod in care AORUS Core Lighting lumineaza vastul ecosistem AORUS.",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("GIGABYTE")).ID,
                    Price = Decimal.Parse("2250")
                },
                new Item
                {
                    Title = "Monitor Curbat Gaming LED VA Samsung Odyssey 27\", Full HD, Display Port, 240Hz, G-Sync, Dark Blue Gray, LC27RG50FQRXEN",
                    Description = "Lider in monitoare curbate, Samsung a condus piata monitoarelor curbate din momentul in care au introdus pentru prima data ecranele inovative in anul 2015. Cu o tehnologie de varf, monitoarele curbate Samsung sunt in permanenta numarul 1 pe piata si prima alegere a consumatorilor.",
                    ManufacturerID = manufacturers.Single(m => m.Name.Contains("SAMSUNG")).ID,
                    Price = Decimal.Parse("22")
                },
            };

            foreach (Item item in items)
            {
                context.Items.Add(item);
            }

            context.SaveChanges();

            var customers = new Customer[] {
                new Customer {
                    ID = 1,
                    Name = "Popescu Marcela",
                    BirthDate = DateTime.Parse("1979-09-01"),
                    PhoneNumber = "0744112233",
                    EmailAddress = "pop.marcela@gmail.com"
                },
                new Customer {
                    ID = 2,
                    Name = "Mihailescu Cornel",
                    BirthDate = DateTime.Parse("1969-07-08"),
                    PhoneNumber = "0744111222",
                    EmailAddress = "mihailescu88@gmail.com"
                },
                new Customer {
                    ID = 3,
                    Name = "Fodor Dorin",
                    BirthDate = DateTime.Parse("1969-07-08"),
                    PhoneNumber = "0744778899",
                    EmailAddress = "dorynutz_fodor11@gmail.com"
                },
                new Customer {
                    ID = 4,
                    Name = "Baldac Cristian",
                    BirthDate = DateTime.Parse("1969-07-08"),
                    PhoneNumber = "0744123456",
                    EmailAddress = "baldac77@gmail.com"
                },
            };

            foreach (Customer customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.SaveChanges();

            var sellers = new Seller[] {
                new Seller {
                    Name = "VEXIO",
                    Address = "Str 1 mai nr 2, Panciu, Vrancea, 625400",
                    PhoneNumber = "021.200 52 00",
                    EmailAddress = "contact@vexio.ro"
                },
                new Seller {
                    Name = "eMAG Romania",
                    Address = "Str. Aviatorilor, nr. 40, Bucuresti",
                    PhoneNumber = "021.200 52 00",
                    EmailAddress = "contact@emag.ro",
                },
                new Seller {
                    Name = "VALI Computers Ltd",
                    Address = "6 Samuil Street, Veliko Turnovo, Veliko Turnovo, 5000",
                    PhoneNumber = "021.200 52 00",
                    EmailAddress = "contact@vali.bg"
                },
            };

            foreach (Seller seller in sellers)
            {
                context.Sellers.Add(seller);
            }

            context.SaveChanges();

            var listedItems = new ListedItem[] {
                new ListedItem {
                    ItemID = items.Single(c => c.Title.Contains("iPhone 13")).ID,
                    SellerID = sellers.Single(i => i.Name.Contains("eMAG")).ID
                },
                new ListedItem {
                    ItemID = items.Single(c => c.Title.Contains("AORUS WATERFORCE X")).ID,
                    SellerID = sellers.Single(i => i.Name.Contains("VALI")).ID
                },
                new ListedItem {
                    ItemID = items.Single(c => c.Title.Contains("AORUS GeForce RTX 3070")).ID,
                    SellerID = sellers.Single(i => i.Name.Contains("eMAG")).ID
                },
                new ListedItem {
                    ItemID = items.Single(c => c.Title.Contains("Laptop Gaming ASUS ROG Zephyrus G14 GA401QEC")).ID,
                    SellerID = sellers.Single(i => i.Name.Contains("eMAG")).ID
                },
                new ListedItem {
                    ItemID = items.Single(c => c.Title.Contains("Placa de baza GIGABYTE X570")).ID,
                    SellerID = sellers.Single(i => i.Name.Contains("eMAG")).ID
                },
                new ListedItem {
                    ItemID = items.Single(c => c.Title.Contains("Monitor Curbat Gaming LED VA Samsung Odyssey")).ID,
                    SellerID = sellers.Single(i => i.Name.Contains("VEXIO")).ID
                },
            };

            foreach (ListedItem listedItem in listedItems)
            {
                context.ListedItems.Add(listedItem);
            }

            context.SaveChanges();

            var orders = new Order[] {
                new Order {
                    ItemID = items.Single(c => c.Title.Contains("iPhone 13")).ID,
                    CustomerID = 1,
                    OrderDate = DateTime.Now,
                    Quantity = 2
                },
                new Order {
                    ItemID = items.Single(c => c.Title.Contains("iPhone 13")).ID,
                    CustomerID = 2,
                    OrderDate = DateTime.Parse("2021-07-08"),
                    Quantity = 1
                },
                new Order {
                    ItemID = items.Single(c => c.Title.Contains("Laptop Gaming ASUS ROG Zephyrus G14 GA401QEC")).ID,
                    CustomerID = 3,
                    OrderDate = DateTime.Parse("2021-08-13"),
                    Quantity = 1
                },
                new Order {
                    ItemID = items.Single(c => c.Title.Contains("Monitor Curbat Gaming LED VA Samsung Odyssey")).ID,
                    CustomerID = 1,
                    OrderDate = DateTime.Parse("2021-06-17"),
                    Quantity = 4
                },
                new Order {
                    ItemID = items.Single(c => c.Title.Contains("AORUS WATERFORCE X")).ID,
                    CustomerID = 4,
                    OrderDate = DateTime.Parse("2021-12-12"),
                    Quantity = 1
                },
                new Order {
                    ItemID = items.Single(c => c.Title.Contains("AORUS GeForce RTX 3070")).ID,
                    CustomerID = 3,
                    OrderDate = DateTime.Parse("2021-12-20"),
                    Quantity = 1
                },
                new Order {
                    ItemID = items.Single(c => c.Title.Contains("AORUS GeForce RTX 3070")).ID,
                    CustomerID = 4,
                    OrderDate = DateTime.Parse("2021-12-01"),
                    Quantity = 1
                },
            };

            foreach (Order order in orders) {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }
}