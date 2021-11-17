export class State {
    public StateID!: number;
    public Name!: string
    public LGAs!: LGA[]
}

export class LGA {
    public LgaID!: number;
    public Name!: string
    public StateID!: number;
}