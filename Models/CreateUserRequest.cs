using System.Xml.Serialization;

namespace BankApp;

[XmlRoot("user")]
public class CreateUserRequest
{
    [XmlElement("firstName")]
    public string FirstName { get; set; }

    [XmlElement("lastName")]
    public string LastName { get; set; }

    [XmlElement("email")]
    public string Email { get; set; }

    [XmlElement("password")]
    public string Password { get; set; }
}

