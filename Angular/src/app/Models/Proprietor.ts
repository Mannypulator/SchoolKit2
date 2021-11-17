import { Gender } from "./Gender";

export class Proprietor {
    public Id!: string;
    public FirstName!: string;
    public LastName!: string;
    public Address!: string;
    public Email!: string;
    public LgaID!: number;
    public Gender = Gender;
  
    public PasswordHash!: string;
}