import api from "../api.helper";

import { ICommunity } from "@models/community.model";

const endpoint = "/api/core/communities/authenticatedUser";

export class CommunitiesService {
  static async getUserCommunities(): Promise<ICommunity[]> {
    return api.get(endpoint);
  }

  // static async getCommunity(communityId: string): Promise<ICommunity> {
  //   return api.get();
  // }
}
