import { runInAction, makeAutoObservable } from "mobx";

import { PostsService } from "@api/services/posts.service";

import { IPost } from "@models/post.model";
import { IComment } from "@models/comment.model";
import { FetchStatus } from "@common/enums/fetch-status.enum";

export class PostState {
  post: IPost = {} as IPost;
  comments: IComment[] = [];
  state = FetchStatus.PENDING;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchPostById(postId: string) {
    this.state = FetchStatus.PENDING;
    try {
      const post = await PostsService.getPostById(postId);
      runInAction(() => {
        this.post = post;
        this.state = FetchStatus.DONE;
      });
    } catch (e) {
      runInAction(() => {
        this.state = FetchStatus.ERROR;
      });
    }
  }

  async fetchPostComments(postId: string) {
    this.state = FetchStatus.PENDING;
    try {
      const comments = await PostsService.getPostComments(postId);
      runInAction(() => {
        this.comments = comments;
        this.state = FetchStatus.DONE;
      });
    } catch (e) {
      runInAction(() => {
        this.state = FetchStatus.ERROR;
      });
    }
  }
}

export const postState = new PostState();
