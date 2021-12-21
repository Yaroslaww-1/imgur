import React from "react";

import { postsState } from "./community.state";

import { Page } from "@components/page";
import { PostsList } from "./post-list";

export const Community: React.FC = () => {
  return (
    <Page>
      <PostsList state={postsState} />
    </Page>
  );
};
