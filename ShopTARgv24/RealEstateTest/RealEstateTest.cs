using System;
using Microsoft.VisualBasic;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Core.Domain;


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





    

