import { IUser } from "./user.model";

interface IAuthenticatedUserReaction {
  isLike: boolean;
}

export interface IPost {
  id: string;
  name: string;
  content: string;
  commentsCount: number;
  likesCount: number;
  dislikesCount: number;
  createdBy: IUser;
  authenticatedUserReaction: IAuthenticatedUserReaction | null;
  imagesUrls: string[];
}
