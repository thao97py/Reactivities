import { Tab } from "semantic-ui-react"
import ProfilePhoto from "./ProfilePhoto";
import { Profile } from "../../app/models/Profile";
import { observer } from "mobx-react-lite";
import ProfileFollowings from "./ProfileFollowings";
import { useStore } from "../../app/stores/store";

interface Props{
    profile:Profile;
}

export default observer(function ProfileContent({profile}:Props){
    const {profileStore} = useStore();

    const panes = [
        {menuItem: 'About', render:() =><Tab.Pane>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</Tab.Pane>}, //render when click on
        {menuItem: 'Photos', render:() =><ProfilePhoto profile={profile}/>},
        {menuItem: 'Events', render:() =><Tab.Pane>Events Content</Tab.Pane>},
        {menuItem: 'Followers', render:() =><ProfileFollowings/>},
        {menuItem: 'Following', render:() =><ProfileFollowings/>}
    ];

    return(
        <Tab
            menu = {{fluid: true, vertical: true}}
            menuPosition='right'
            panes={panes}
            onTabChange={(_,data) =>profileStore.setActiveTab(data.activeIndex as number) }
        />
    )
})