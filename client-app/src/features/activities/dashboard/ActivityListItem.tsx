import { Link } from 'react-router-dom';
import { Button, Icon, Item, ItemDescription, Label, Segment } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';
import format from 'date-fns/format';
import ActivityListItemAttendee from './ActivityListItemAttendee';

interface Props{
    activity: Activity;
}

export default function ActivityListItem({activity}: Props){
    // const {activityStore} = useStore();
    // const {deleteActivity, loading} = activityStore;
    // const [target,setTarget] = useState('');
    
    // function handleActivityDelete(e:SyntheticEvent<HTMLButtonElement>,id:string){
    //     setTarget(e.currentTarget.name);
    //     deleteActivity(id);
    // }

    return(
        <Segment.Group>
            <Segment>
                {activity.isCancelled && 
                   <Label attached='top' color='red' content='Cancelled' style={{textAlign: 'center'}} /> 
                }
                <Item.Group>
                    <Item>
                        <Item.Image style={{marginBottom: 3}} size='tiny' circular src={activity.host?.image || '/assets/user.png'}/>
                        <Item.Content>
                            <Item.Header as={Link} to={`/activities/${activity.id}`}>{activity.title}
                            </Item.Header>
                            <Item.Description>Hosted by <Link to={`/profiles/${activity.hostUsername}`}>{activity.host?.displayName}</Link></Item.Description>
                            {activity.isHost && (
                                <ItemDescription>
                                    <Label basic color='orange' >
                                        You are hosting this activity
                                    </Label>
                                </ItemDescription>
                            )}
                            {activity.isGoing && !activity.isHost &&(
                                <ItemDescription>
                                    <Label basic color='green' >
                                        You are going to this activity
                                    </Label>
                                </ItemDescription>
                            )}
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='clock'></Icon>{format (activity.date!, 'dd MMM yyyy h:mm aa')}
                    <Icon name='marker'></Icon>{activity.venue}
                </span>
            </Segment>
            <Segment secondary>
                <ActivityListItemAttendee attendees={activity.attendees!}></ActivityListItemAttendee>
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button
                    as={Link} to={`/activities/${activity.id}`}
                    color='teal'
                    floated='right'
                    content='View'
                />
            </Segment>
        </Segment.Group>
        // <Item key={activity.id}>
        //                 <Item.Content>
        //                     <Item.Header as='a'>{activity.title}</Item.Header>
        //                     <Item.Meta>{activity.date}</Item.Meta>
        //                     <Item.Description>
        //                         <div>{activity.description}</div>
        //                         <div>{activity.city}, {activity.venue}</div>
        //                     </Item.Description>
        //                     <Item.Extra>
        //                         <Button as={Link} to={`/activities/${activity.id}`} floated='right' content='View' color="blue"></Button>
        //                         <Button 
        //                             name={activity.id}
        //                             loading={loading && target== activity.id} 
        //                             onClick={(e) => handleActivityDelete(e, activity.id)} 
        //                             floated='right' 
        //                             content='Delete' 
        //                             color="red">
        //                         </Button>
        //                         <Label basic content={activity.category}></Label>
        //                     </Item.Extra>
        //                 </Item.Content>
        //             </Item>
    )
}