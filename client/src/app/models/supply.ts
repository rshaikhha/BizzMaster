export interface Suppliers{
    Id : number;

        title : string;
        FullTitle : string;
        Country : string;
        Email : string;
        Address : string;
        Website : string;
        Contacts : Contact[];
}

export interface Contact{
    FirstName : string;
    LastName : string;
    Initials : string;
    Position : string;
    Email : string;
    Mobile : string;
    Mobile2 : string;
    Mobile3 : string;
    WhatsApp : string;
    IsMale: string;
}