import { IUser } from "./user.model";

export interface ICommunity {
  id: string;
  name: string;
  description: string;
  membersCount: number;
  isAuthenticatedUserJoined: boolean;
  createdBy: IUser;
}
