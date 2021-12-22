import api from "../api.helper";

import { ICommunity } from "@models/community.model";

const endpoint = "/api/core/communities";

export class CommunitiesService {
  static async getUserCommunities(): Promise<ICommunity[]> {
    return api.get(endpoint + "/authenticatedUser");
  }

  static async getAllCommunities(): Promise<ICommunity[]> {
    return api.get(endpoint);
  }

  static async getCommunityById(communityId: string): Promise<ICommunity> {
    return api.get(endpoint + "/" + communityId);
  }

  static async joinCommunity(communityId: string): Promise<void> {
    api.post(endpoint + "/" + communityId + "/join", "");
  }

  static async leaveCommunity(communityId: string): Promise<void> {
    api.post(endpoint + "/" + communityId + "/leave", "");
  }
}
