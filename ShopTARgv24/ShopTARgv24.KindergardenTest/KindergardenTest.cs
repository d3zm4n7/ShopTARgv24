using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Data;
using Xunit;
using System;
using System.Threading.Tasks;

namespace ShopTARgv24.KindergardenTest
{
    public class KindergardenTest : TestBase
    {
        // ТЕСТ 1: Проверка успешного создания (Create)
        [Fact]
        public async Task Should_CreateKindergarden_WhenReturnResult()
        {
            // Arrange
            KindergardenDto dto = MockKindergardenData();

            // Act
            var result = await Svc<IKindergardensServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Id);
            Assert.Equal(dto.GroupName, result.GroupName);
            Assert.Equal(dto.KindergardenName, result.KindergardenName);
        }

        // ТЕСТ 2: Проверка получения по ID (GetById)
        [Fact]
        public async Task Should_GetByIdKindergarden_WhenReturnsEqual()
        {
            // Arrange
            var created = await Svc<IKindergardensServices>().Create(MockKindergardenData());
            Guid id = (Guid)created.Id;

            // Act
            var result = await Svc<IKindergardensServices>().DetailAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal(created.TeacherName, result.TeacherName);
        }

        // ТЕСТ 3: Проверка удаления (Delete)
        [Fact]
        public async Task Should_DeleteByIdKindergarden_WhenDeleteKindergarten()
        {
            // Arrange
            var created = await Svc<IKindergardensServices>().Create(MockKindergardenData());
            Guid id = (Guid)created.Id;

            // Act
            var deleted = await Svc<IKindergardensServices>().Delete(id);
            var resultAfterDelete = await Svc<IKindergardensServices>().DetailAsync(id);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(created.Id, deleted.Id);
            Assert.Null(resultAfterDelete); 
        }

        // ТЕСТ 4: Проверка обновления данных (Update)
        [Fact]
        public async Task Should_UpdateKindergarden_WhenUpdateData()
        {
            // Arrange
            var created = await Svc<IKindergardensServices>().Create(MockKindergardenData());

           
            var context = Svc<ShopTARgv24Context>();
            context.ChangeTracker.Clear();
          
            var updateDto = MockUpdateKindergardenData();
            updateDto.Id = created.Id; 

            // Act
            var updated = await Svc<IKindergardensServices>().Update(updateDto);

            // Assert
            Assert.NotNull(updated);
            Assert.Equal(updateDto.GroupName, updated.GroupName); 
            Assert.NotEqual(created.GroupName, updated.GroupName); 
            Assert.Equal(updateDto.ChildrenCount, updated.ChildrenCount);
        }

        // ТЕСТ 5: Проверка, что нельзя получить несуществующую запись (Negative Test)
        [Fact]
        public async Task ShouldNot_GetKindergarden_WhenIdNotExists()
        {
            // Arrange
            Guid fakeId = Guid.NewGuid();

            // Act
            var result = await Svc<IKindergardensServices>().DetailAsync(fakeId);

            // Assert
            Assert.Null(result);
        }

        // ТЕСТ 6: Проверка, что при обновлении сохраняется дата создания, а дата изменения обновляется
        [Fact]
        public async Task Should_PreserveCreatedAt_And_UpdateUpdatedAt_When_UpdateKindergarden()
        {
            // Arrange
            var created = await Svc<IKindergardensServices>().Create(MockKindergardenData());

            await Task.Delay(100);

            var context = Svc<ShopTARgv24Context>();
            context.ChangeTracker.Clear();

            var updateDto = MockUpdateKindergardenData();
            updateDto.Id = created.Id;
            updateDto.CreatedAt = created.CreatedAt;

            // Act
            var updated = await Svc<IKindergardensServices>().Update(updateDto);

            // Assert
            Assert.NotNull(updated);
            Assert.Equal(created.CreatedAt, updated.CreatedAt);
            Assert.NotEqual(created.UpdatedAt, updated.UpdatedAt);
            Assert.True(updated.UpdatedAt > created.CreatedAt);
        }


        private KindergardenDto MockKindergardenData()
        {
            return new KindergardenDto()
            {
                GroupName = "Lepatriinud",
                ChildrenCount = 24,
                KindergardenName = "Tallinna 1. Lasteaed",
                TeacherName = "Maali Maalakas",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        private KindergardenDto MockUpdateKindergardenData()
        {
            return new KindergardenDto()
            {
                GroupName = "Mesimummud",
                ChildrenCount = 20,
                KindergardenName = "Tartu Lasteaed",
                TeacherName = "Jüri Juurikas",
            };
        }
    }
}