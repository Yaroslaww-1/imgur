interface IRole {
  id: string;
  name: string;
}

export interface IUser {
  id: string;
  email: string;
  name: string;
  roles: IRole[];
}
