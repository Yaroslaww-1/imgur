import { IUser } from "./user.model";

interface IAuthenticatedUserReaction {
  isLike: boolean;
}

export interface IComment {
  id: string;
  content: string;
  likesCount: number;
  dislikesCount: number;
  createdBy: IUser;
  authenticatedUserReaction: IAuthenticatedUserReaction | null;
}
