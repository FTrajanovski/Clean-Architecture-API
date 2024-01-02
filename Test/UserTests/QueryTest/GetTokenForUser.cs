namespace Test.UserTests.QueryTest
{
    /*
    [TestFixture]
    public class GetTokenForUser
    {
        private GetUserTokenQueryHandler _handler;
        private MockDatabase _mockDatabase;
        private JwtTokenGenerator _jwtGenerator;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _jwtGenerator = new JwtTokenGenerator();
            _handler = new GetUserTokenQueryHandler(_mockDatabase, _jwtGenerator);
        }

        [Test]
        public async Task Handle_Generate_Token_For_Valid_User()
        {
            //Arrange
            var username = "Tobias";
            var password = "123password";

            var query = new GetUserTokenQuery(username, password);

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task Handle_Invalid_User()
        {
            //Arrange
            var username = "SuperTaskigHemskDunderHacker";
            var password = "123password";

            var query = new GetUserTokenQuery(username, password);

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsNull(result);
        }
    }
    */
}