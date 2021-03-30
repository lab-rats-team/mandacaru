public class CredentialsModel {
	public string Email { get; set; }
	public string Password { get; set; }

	public CredentialsModel(string e, string p){
		Email = e;
		Password = p;
	}
}
