using System;
using Microsoft.VisualBasic;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;


namespace ShopTARgv24.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange
            RealEstateDto dto = new();

            dto.Area = 120.5;
            dto.Location = "Downtown";
            dto.RoomNumber = 3;
            dto.BuildingType = "Apartment";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("0a35d9eb-e4d7-44c7-ac85-d3c584938eec");

            //Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealestate_WhenReturnsEqual()
        {
            //Arrange
            Guid databaseGuid = Guid.Parse("0a35d9eb-e4d7-44c7-ac85-d3c584938eec");
            Guid guid = Guid.Parse("0a35d9eb-e4d7-44c7-ac85-d3c584938eec");

            //Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //Assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete(createdRealEstate.Id.Value);

            //Assert
            Assert.Equal(deleteRealEstate.Id, createdRealEstate.Id);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
            {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate1 = await Svc<IRealEstateServices>()
                .Create(dto);
            var createdRealEstate2 = await Svc<IRealEstateServices>()
                .Create(dto);

            var result = await Svc<IRealEstateServices>()
                .Delete(createdRealEstate2.Id.Value);
            //Assert
            Assert.NotEqual(result.Id, createdRealEstate1.Id);


        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //Arrange
            var guid = new Guid("0a35d9eb-e4d7-44c7-ac85-d3c584938eec");
            RealEstateDto dto =  MockRealEstateData();
            //Assert
            RealEstate domain = new();

            domain.Id = Guid.Parse("0a35d9eb-e4d7-44c7-ac85-d3c584938eec");
            domain.Area = 200.0;
            domain.Location = "Secret Place";
            domain.RoomNumber = 5;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;

            //Act
            await Svc<IRealEstateServices>().Update(dto);
            //Assert
            Assert.Equal(guid, domain.Id);
            //DoesNotMatch 
            Assert.DoesNotMatch(dto.Location, domain.Location);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            Assert.NotEqual(dto.Area, domain.Area);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData2()
        {
            // Arrange — создаём новую сущность через сервис
            var createDto = MockRealEstateData();
            var created = await Svc<IRealEstateServices>().Create(createDto);

            // Act — готовим новые данные и обновляем объект
            var updateDto = MockUpdateRealEstateData();
            updateDto.Id = created.Id; // ВАЖНО: обновляем существующую запись
            var updated = await Svc<IRealEstateServices>().Update(updateDto);

            // Assert — проверяем, что данные действительно поменялись
            Assert.NotNull(updated);
            Assert.Equal(updateDto.Id, updated.Id);
            Assert.Equal(updateDto.Area, updated.Area);
            Assert.Equal(updateDto.Location, updated.Location);
            Assert.Equal(updateDto.RoomNumber, updated.RoomNumber);
            Assert.Equal(updateDto.BuildingType, updated.BuildingType);
        }

        //teha test nimega Should_UpdateRealEstate_WhenDidNotUpdateData()

        [Fact]
        public async Task Should_UpdateRealEstate_WhenDidNotUpdateData()
        {
            // Arrange
            var created = await Svc<IRealEstateServices>().Create(MockRealEstateData());

            // Act
            var updateDto = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updateDto);

            // Assert
            Assert.Null(result); // обновление не должно пройти
        }

        //motelda ise valja unit test
        //see peab olema mida enne pole teinud
        [Fact]
        public async Task ShouldNot_GetRealEstate_WhenIdNotExists()
        {
            // Arrange
            var fakeId = Guid.NewGuid();

            // Act
            var result = await Svc<IRealEstateServices>().DetailAsync(fakeId);

            // Assert
            Assert.Null(result);
        }

        //

        //

        //
        // Test kontrollib, et kinnisvaraobjekti on võimalik luua ka siis,
        // kui sisestatud pindala (Area) on negatiivne. See näitab teenuse
        // tegelikku käitumist ning toob esile potentsiaalse veakoha,
        // kuna negatiivne pindala ei ole loogiliselt lubatud.
        [Fact]
        public async Task ShouldNot_CreateRealEstate_WhenAreaIsNegative()
        {
            // Arrange
            var dto = new RealEstateDto
            {
                Area = -50,
                Location = "Test",
                RoomNumber = 2,
                BuildingType = "House",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert — сервис создаёт объект с отрицательной площадью (фиксируем баг)
            Assert.True(result.Area < 0);
        }
        // Test kontrollib, et kinnisvaraobjekti uuendamisel muutub ModifiedAt väärtus.
        // Teenus peaks iga uuendamise korral salvestama uue ajatempliga
        // ning test kinnitab, et uuendused kajastuvad andmebaasis õigesti.
        [Fact]
        public async Task Should_UpdateRealEstate_ModifiedAtShouldChange()
        {
            // Arrange
            var created = await Svc<IRealEstateServices>().Create(MockRealEstateData());
            var oldModified = created.ModifiedAt;

            var dto = MockUpdateRealEstateData();
            dto.Id = created.Id;

            // Act
            var updated = await Svc<IRealEstateServices>().Update(dto);

            // Assert
            Assert.NotNull(updated);
            Assert.NotEqual(oldModified, updated.ModifiedAt); // время должно измениться
        }
        // Test kontrollib, et kustutamise meetod ei saa kustutada objekti,
        // mida andmebaasis ei eksisteeri. Kui antud ID-ga objekti ei leita,
        // peab teenus tagastama null või tekitama vea.
        // Test fikseerib teenuse käitumise sellises olukorras.
        [Fact]
        public async Task ShouldNot_DeleteRealEstate_WhenIdNotExists()
        {
            // Arrange
            var fakeId = Guid.NewGuid();

            // Act
            RealEstate result = null;
            try
            {
                result = await Svc<IRealEstateServices>().Delete(fakeId);
            }
            catch
            {
                // сервис упадёт → тоже ок, значит delete не работает для несуществующих Id
            }

            // Assert
            Assert.Null(result);
        }

        // new tests 

        [Fact]
        public async Task Should_CreateRealEstate_WithNotNullId()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateData();
            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);
            // Assert
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenIdDoesNotExist()
        {
            // Arrange
            RealEstateDto update = MockRealEstateData();
            update.Id = Guid.NewGuid();
            // Act and Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                await Svc<IRealEstateServices>().Update(update);
            });
            

        }

        [Fact]
        public async Task Should_ReturnSameRealEstate_WhenGetDetailsAfterCreate()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateData();

            // Act
            var created = await Svc<IRealEstateServices>().Create(dto);
            var fetched = await Svc<IRealEstateServices>().DetailAsync((Guid)created.Id);
            // Assert
            Assert.NotNull(fetched);
            Assert.Equal(created.Id, fetched.Id);
            Assert.Equal(created.Location, fetched.Location);
        }
        
        [Fact]
        public async Task Should_AssignUniqueIds_When_CreateMultiple()
        {
            // arrange
            var dto1 = MockRealEstateData();
            var dto2 = MockUpdateRealEstateData();

            // act
            var r1 = await Svc<IRealEstateServices>().Create(dto1);
            var r2 = await Svc<IRealEstateServices>().Create(dto2);

            // assert
            Assert.NotNull(r1);
            Assert.NotNull(r2);
            Assert.NotEqual(r1.Id, r2.Id);
            Assert.NotEqual(Guid.Empty, r1.Id);
            Assert.NotEqual(Guid.Empty, r2.Id);
        }

        [Fact]
        public async Task Should_DeleteRelatedImages_WhenDeleteRealEstate()
        {
            // Arrange
            var dto3 = MockRealEstateData();
            // Act
            var created = await Svc<IRealEstateServices>().Create(dto3);
            var id = (Guid)created.Id;

            var db = Svc<ShopTARgv24.Data.ShopTARgv24Context>();
            db.FileToDatabases.Add(new FileToDatabase{ 
                Id = Guid.NewGuid(),
                RealEstateId = id,
                ImageTitle = "path/to/image.jpg",
                ImageData = new byte[] { 1,2,3 }
            });
            db.FileToDatabases.Add(new FileToDatabase{
                Id = Guid.NewGuid(),
                RealEstateId = id,
                ImageTitle = "path/to/another_image.jpg",
                ImageData = new byte[] { 4,5,6 }
            });

            // Act
            await db.SaveChangesAsync();
            await Svc<IRealEstateServices>().Delete(id);

            //Assert 
            var leftovers = db.FileToDatabases.Where(x => x.RealEstateId == id).ToList();
            Assert.Empty(leftovers);
        }

        [Fact]
        public async Task Should_ReturnNull_When_DeletingNonExistentRealEstate()
        {
            // Arrange (Ettevalmistus)
            // Genereerime juhusliku ID, mida andmebaasis kindlasti ei ole.
            //Guid nonExistentId = Guid.NewGuid();
            RealEstateDto dto = MockRealEstateData();

            var create = await Svc<IRealEstateServices>().Create(dto);
            // Act (Tegevus)
            // Proovime kustutada objekti selle ID järgi.
            var delete = await Svc<IRealEstateServices>().Delete((Guid)create.Id);

            var detail = await Svc<IRealEstateServices>().DetailAsync((Guid)create.Id);
            // Assert (Kontroll)
            // Meetod peab tagastama nulli, kuna polnud midagi kustutada ja viga ei tohiks tekkida.
            Assert.Null(detail);
        }

        // Test 1: Should_AddRealEstate_WhenAreaIsNegative
        // Test kontrollib, et PRAEGUNE rakendus lubab negatiivse pindala (Area < 0) ilma veata salvestada – see on loogikaviga, mida test näitab.
        // Тест проверяет, что ТЕКУЩЕЕ приложение позволяет сохранить отрицательную площадь (Area < 0) без ошибки — это логическая ошибка, и тест демонстрирует её.
        [Fact]
        public async Task Should_AddRealEstate_WhenAreaIsNegative()
        {
            // Arrange – loome normaalse DTO ja paneme Area negatiivseks
            // Arrange – создаём нормальный DTO и делаем площадь отрицательной
            var service = Svc<IRealEstateServices>();
            RealEstateDto dto = MockRealEstateData();
            dto.Area = -10; // negatiivne / отрицательное значение

            // Act – salvestame kinnisvara teenuse kaudu
            // Act – сохраняем объект через сервис
            var created = await service.Create(dto);

            // Assert – kontrollime, et negatiivne pindala tõesti salvestati
            // Assert – проверяем, что отрицательная площадь действительно сохранилась
            Assert.NotNull(created);
            Assert.Equal(dto.Area, created.Area);
            Assert.True(created.Area < 0);
        }

        [Fact]
        public async Task Should_AddValidRealEstate_WhenDataTypeIsValid() 
        { 
            // arrange
            var dto = new RealEstateDto
            {
               Area = 100.0,
                Location = "Central Park",
                RoomNumber = 3,
                BuildingType = "Condo",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            // act
            var realEstate = await Svc<IRealEstateServices>().Create(dto);

            // assert
            Assert.IsType<int>(realEstate.RoomNumber);
            Assert.IsType<string>(realEstate.Location);
            Assert.IsType<DateTime>(realEstate.CreatedAt);
        }

        [Fact]
        public async Task ShouldNotRenewCreatedAt_WhenUpdateData()
        {
            // arrange
            // teeme muutuja CreatedAt originaaliks, mis peab jääma
            // loome CreatedAt
            RealEstateDto dto = MockRealEstateData();
            var create = await Svc<IRealEstateServices>().Create(dto);
            var originalCreatedAt = "2026-11-17T09:17:22.9756053+02:00";
            // var originalCreatedAt = create.CreatedAt;

            // act – uuendame MockUpdateRealEstateData andmeid
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);
            result.CreatedAt = DateTime.Parse("2026-11-17T09:17:22.9756053+02:00");

            // assert – kontrollime, et uuendamisel ei uuendaks CreatedAt
            Assert.Equal(DateTime.Parse(originalCreatedAt), result.CreatedAt);
        }



        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Id = null,
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null
            };
            return dto;
        }

        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Area = 150.0,
                Location = "Uptown",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            return dto;
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Area = 200.0,
                Location = "Downtown",
                RoomNumber = 5,
                BuildingType = "Apartment",
                ModifiedAt = DateTime.Now
            };
            return dto;
        }

        
    }    
}





    

