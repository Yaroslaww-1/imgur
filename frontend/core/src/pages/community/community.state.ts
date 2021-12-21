import { runInAction, makeAutoObservable } from "mobx";

import { PostsService } from "@api/services/posts.service";
import { CommunitiesService } from "@api/services/communities.service";

import { IPost } from "@models/post.model";
import { ICommunity } from "@models/community.model";
import { FetchStatus } from "@common/enums/fetch-status.enum";

export class CommunityState {
  community: ICommunity = {} as ICommunity;
  posts: IPost[] = [];
  state = FetchStatus.PENDING;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchCommunity(communityId: string) {
    this.state = FetchStatus.PENDING;
    try {
      const posts = await CommunitiesService.getCommunity(communityId);
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

  async fetchPosts(communityId: string) {
    this.state = FetchStatus.PENDING;
    try {
      const posts = await PostsService.getCommunityPosts(communityId);
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

export const postsState = new CommunityState();
