namespace ValtechBaseLine.Model.Components
{
    public interface IRecaptcha
    {
         bool ValidateCaptcha(string ResponseLink,string PrivateKey);
    }
}
