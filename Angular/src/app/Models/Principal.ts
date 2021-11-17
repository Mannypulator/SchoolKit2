import { Gender } from "./Gender";
import { School } from "./School";

export class Principal {
    public FirstName!: string;
    public LastName!: string;
    public Address!: string;
    public Email!: string;
    public SchoolID!: number;
    public LgaID!: number;
    public Gender = Gender;
    public PasswordHash!: string;
    
  
}