import { SchoolType } from "./SchoolType";

export class School {
        public name!: string;
        public address!: string;
        public lgaID!: number;
        public showPosition!: boolean;
        public append!: string;
        public proprietorID!: number;
        public type = SchoolType;
        public adminID!: number;
        public code!: number;
        public ssCompulsories!: string[];
        public ssDrops!: string[];
}
