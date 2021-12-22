import React, { useEffect, useState } from "react";

import { ICommunity } from "@models/community.model";

import styles from "./styles.module.scss";
import { SimpleButton } from "@components/buttons/simple-button";

interface IProps {
  community: ICommunity;
  subscribed: boolean;
}

export const CommunityItem: React.FC<IProps> = ({ community, subscribed }) => {
  return (
    <div className={styles.item}>
      <div>
        <div className={styles.name}>{community.name}</div>
        <div className={styles.description}>{community.description}</div>
      </div>
      <div className={styles.subscribe}>
        {subscribed ? (
          <SimpleButton text={"Unsubscribe"} size={"small"} />
        ) : (
          <SimpleButton text={"Subscribe"} size={"small"} />
        )}
      </div>
    </div>
  );
};
