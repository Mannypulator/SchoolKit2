import { Gender } from "./Gender";

export class Principal {
    public FirstName!: string;
    public LastName!: string;
    public Address!: string;
    public Email!: string;
    public SchoolID!: number;
    public LgaID!: number;
    public gender = Gender;
    public passwordHarsh!: string;

}