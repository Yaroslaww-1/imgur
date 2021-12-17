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
  authenticatedUserReaction: IAuthenticatedUserReaction | null;
}
