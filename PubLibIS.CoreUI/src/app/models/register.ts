export class RegisterModel {
  constructor(
    public email?: string, 
    public password?: string,
    public confirmPassword?: string,
    public address?: string,
    public name?: string,
    public admin?: boolean
  ) { }
}
