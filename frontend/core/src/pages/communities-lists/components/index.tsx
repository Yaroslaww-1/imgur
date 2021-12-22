import React, { useEffect, useState } from "react";

import { CommunitiesService } from "@api/services/communities.service";
import { ICommunity } from "@models/community.model";

import { Page } from "@components/page";
import { CommunityItem } from "./community-item";

import styles from "./styles.module.scss";

export const CommunitiesLists: React.FC = () => {
  const [userCommunitiesSwitcher, setUserCommunitiesSwitcher] = useState(true);
  const [userCommunities, setUserCommunities] = useState<ICommunity[]>();
  const [allCommunities, setAllCommunities] = useState<ICommunity[]>();

  useEffect(() => {
    CommunitiesService.getAllCommunities().then(communities => {
      setAllCommunities(communities);
    });
    CommunitiesService.getUserCommunities().then(communities => {
      setUserCommunities(communities);
    });
  }, []);

  if (userCommunitiesSwitcher) {
    return (
      <Page>
        <input type="checkbox" id="switch" />
        <div className={styles.app}>
          <div className={styles.content}>
            <label
              htmlFor="switch"
              onClick={() => {
                setUserCommunitiesSwitcher(!userCommunitiesSwitcher);
              }}
            >
              <div className={styles.toggle}></div>
              <div className={styles.names}>
                <p className={styles.first}>My communities</p>
                <p className={styles.second}>All communities</p>
              </div>
            </label>
          </div>
        </div>
        {userCommunities?.map(community => (
          <CommunityItem
            key={community.id}
            community={community}
            subscribed={true}
          />
        ))}
      </Page>
    );
  } else {
    return (
      <Page>
        <input type="checkbox" id="switch" />
        <div className={styles.app}>
          <div className={styles.content}>
            <label
              htmlFor="switch"
              onClick={() => {
                setUserCommunitiesSwitcher(!userCommunitiesSwitcher);
              }}
            >
              <div className={styles.toggle}></div>
              <div className={styles.names}>
                <p className={styles.first}>My communities</p>
                <p className={styles.second}>All communities</p>
              </div>
            </label>
          </div>
        </div>
        {userCommunities?.map(community => (
          <CommunityItem
            key={community.id}
            community={community}
            subscribed={false}
          />
        ))}
      </Page>
    );
  }
};
