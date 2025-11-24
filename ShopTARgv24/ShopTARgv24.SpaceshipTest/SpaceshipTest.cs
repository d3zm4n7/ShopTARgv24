using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using System.Globalization;
using Xunit.Sdk;

namespace ShopTARgv24.SpaceshipTest
{
    public class SpaceShipTest : TestBase
    {
        public static SpaceshipDto SpaceshipDto1()
        {
            return new()
            {
                Name = "Unit",
                TypeName = "R45",
                BuiltDate = DateTime.ParseExact("15.01.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Crew = 5,
                EnginePower = 50,
                Passengers = 25,
                InnerVolume = 450,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

        [Fact]
        public async Task ShoulNot_AddEmptySpaceship_WhenReturnResult()
        {
            // Arrange 
            SpaceshipDto dto = SpaceshipDto1();

            // Act 
            var result = await Svc<ISpaceshipsServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        //ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
        // что при чтении по Id первой записи мы не получаем вторую (то есть сервис реально возвращает правильный объект по Id).
        [Fact]
        public async Task ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
        {
            // Arrange – создаём две разные записи недвижимости
            var service = Svc<ISpaceshipsServices>();

            SpaceshipDto dto1 = SpaceshipDto1();

            var created1 = await service.Create(dto1);

            // Act – берём из базы по Id только первую запись
            var result = await service.DetailAsync((Guid)created1.Id); // ≈ created1.Id.Value

            // Assert – убеждаемся, что найденная запись НЕ равна второй
            Assert.NotNull(result);
        }


        //Should_GetByIdSpaceship_WhenReturnsEqal()
        // что при чтении по Id мы получаем именно ту же запись (Id совпадает).
        [Fact]
        public async Task Should_GetByIdSpaceship_WhenReturnsEqal()
        {
            // Arrange – создаём одну запись и запоминаем её Id
            var service = Svc<ISpaceshipsServices>();

            SpaceshipDto dto = SpaceshipDto1();


            var created = await service.Create(dto);

            // Act – читаем объект по этому же Id через DetailAsync
            var result = await service.DetailAsync((Guid)created.Id); // ≈ created1.Id.Value

            // Assert – запись нашлась и Id совпадает
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
        }

        //Should_DeleteByIdSpaceship_WhenDeleteRealestate()
        // что после удаления по Id запись больше не находится.
        [Fact]
        public async Task Should_DeleteByIdSpaceship_WhenDeleteRealestate()
        {
            // Arrange – создаём запись, чтобы потом её удалить
            var service = Svc<ISpaceshipsServices>();

            SpaceshipDto dto = SpaceshipDto1();

            var created = await service.Create(dto);

            // Act – удаляем по Id
            await service.Delete((Guid)created.Id);

            // и пробуем прочитать снова
            var resultAfterDelete = await service.DetailAsync((Guid)created.Id);

            // Assert – после удаления объект больше не должен существовать
            Assert.Null(resultAfterDelete);
        }

        //ShouldNot_DeleteByIdSpaceship_WhenDidNotDeleteRealEsrare()
        // Что нужно добавить запись и удалить по не существующими Id
        [Fact]
        public async Task ShouldNot_DeleteByIdSpaceship_WhenDidNotDeleteRealEsrare()
        {
            // Arrange – создаём две записи
            var service = Svc<ISpaceshipsServices>();

            SpaceshipDto dto1 = SpaceshipDto1();

            var created1 = await service.Create(dto1);

            // Act – пробуем удалить по НЕсуществующему Id
            var fakeId = Guid.NewGuid(); // такого Id нет в базе

            try
            {
                await service.Delete(fakeId);
            }
            catch (ArgumentNullException)
            {
                // Это ожидаемое поведение текущей реализации Delete,
                // просто игнорируем исключение в рамках теста.
            }

            // Assert – наша настоящая запись всё ещё существует
            var fromDb = await service.DetailAsync((Guid)created1.Id);

            Assert.NotNull(fromDb);
            Assert.Equal(created1.Id, fromDb.Id);

        }
    }
}
