import React from "react";

import { postState } from "./post.state";

import { PostContainer } from "./containers";
import { Page } from "@components/page";

export const Post: React.FC = () => {
  return (
    <Page>
      <PostContainer state={postState} />
    </Page>
  );
};
