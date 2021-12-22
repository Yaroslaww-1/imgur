import React from "react";

import { postsState } from "./posts.state";

import { Page } from "@components/page";
import { PostsList } from "./components/post-list";

export const Home: React.FC = () => {
  return (
    <Page>
      <PostsList state={postsState} />
    </Page>
  );
};
