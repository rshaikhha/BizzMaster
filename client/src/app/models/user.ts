export interface User {
    username: string;
    token: string;
    roles: string[];
    complete: boolean;
    firstName: string;
    lastName: string;
    email: string;
    avatar: string;
}