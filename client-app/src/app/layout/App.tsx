import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import { ToastContainer } from 'react-toastify';

function App() {
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
  const location = useLocation();

  return (
    // <> </> : fragment component
    <>
      <ToastContainer position='bottom-right' hideProgressBar theme='colored'/>
      {location.pathname === '/' ? <HomePage /> : (
        <>
          <NavBar ></NavBar>
          <Container style={{ marginTop: '7em' }}>
            <Outlet />
          </Container>
        </>
      )}
    </>
  );
}

export default observer(App);
