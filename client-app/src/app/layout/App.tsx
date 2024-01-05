import { useEffect } from 'react'
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashBoard';
import LoadingComponent from './LoadingComponent';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';

function App() {
  const {activityStore} = useStore();
  // const [activities, setActivities] = useState<Activity[]>([]);
  // const [submitting,setSubmitting] = useState(false);

  useEffect(() => {
    activityStore.loadActivities();
  }, [activityStore]);

  // function handleSelectActivity(id: string){
  //   setSelectedActivity(activities.find(x =>x.id === id))
  // }

  // function handleCancelSelectActivity(){
  //   setSelectedActivity(undefined)
  // }

  // function handleFormOpen(id?: string){
  //   id ? handleSelectActivity(id) : handleCancelSelectActivity();
  //   setEditMode(true);
  // }

  // function handleFormClose(){
  //   setEditMode(false);
  // }

  // function handleDeleteActivity(id:string){
  //   setSubmitting(true);
  //   agent.Activities.delete(id).then(() => {
  //     setActivities([...activities.filter(x => x.id != id)]);
  //     setSubmitting(false);
  //   })
  // }

  if (activityStore.loadingInitial) return <LoadingComponent content='Loading ...'/>;

  return (
    // <> </> : fragment component
    <>
      <NavBar ></NavBar>
      <Container style={{ marginTop: '7em' }}>
        <ActivityDashboard 
        />
      </Container>
    </>
  )
}

export default observer(App);
