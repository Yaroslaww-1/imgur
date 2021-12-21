import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react-lite";

import { CommunityState } from "pages/community/community.state";

import styles from "./styles.module.scss";

interface IProps {
  state: CommunityState;
}

interface IUrlParams {
  communityId: string;
}

export const PostsList: React.FC<IProps> = observer(({ state }) => {
  const { communityId } = useParams<IUrlParams>();

  useEffect(() => {
    state.fetchCommunity(communityId);
  }, []);

  return (
    <div className={styles.details}>
      <div className={styles.imageWrapper}>
        <i className="fa fa-users fa-6"></i>
      </div>
      <div className={styles.info}>
        <div className={styles.label}>{state.community.name}</div>
        <div className={styles.label}>{state.community.description}</div>
        <div className={styles.label}>{state.community.membersCount}</div>
        <div className={styles.createdBy}>{state.community.createdBy.name}</div>
      </div>
    </div>
  );
});
