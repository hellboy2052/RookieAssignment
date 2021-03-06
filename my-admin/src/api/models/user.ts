export interface User {
    username: string;
    fullName: string;
    token: string;
    roles: string[];
}

export interface UserFormValues {
    email: string;
    password: string;
    fullname?: string;
    username?: string;
}

export interface UserData {
    email: string;
    username: string;
    fullname: string;
    roles: string[];
}