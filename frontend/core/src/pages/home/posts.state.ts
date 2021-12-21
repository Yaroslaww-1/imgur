import { runInAction, makeAutoObservable } from "mobx";

import { PostsService } from "@api/services/posts.service";

import { IPost } from "@models/post.model";
import { FetchStatus } from "@common/enums/fetch-status.enum";

export class PostsState {
  posts: IPost[] = [];
  state = FetchStatus.PENDING;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchPosts() {
    this.state = FetchStatus.PENDING;
    try {
      const posts = await PostsService.getAllPosts();
      runInAction(() => {
        this.posts = posts;
        this.state = FetchStatus.DONE;
      });
    } catch (e) {
      runInAction(() => {
        this.state = FetchStatus.ERROR;
      });
    }
  }
}

export const postsState = new PostsState();
