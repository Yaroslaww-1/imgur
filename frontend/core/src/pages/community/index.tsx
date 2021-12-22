import React from "react";

import { communityState } from "./community.state";

import { PostsList } from "./post-list";
import { CommunityDetails } from "./community-details";

import { Page } from "@components/page";

export const Community: React.FC = () => {
  return (
    <Page>
      <CommunityDetails state={communityState} />
      <PostsList state={communityState} />
    </Page>
  );
};
