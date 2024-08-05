function Home(): React.JSX.Element {
  return (
    <div className="container-fluid d-flex align-items-center justify-content-center vh-100">
      <div className="text-center">
        <img
          src="https://cache.clinic.co/assets/img/logo.png"
          alt="Clinic"
          className="img-fluid rounded-circle mb-4"
          style={{ width: "200px" }}
        />
        <h1 className="display-4 mb-4">Welcome to Clinic WebApp</h1>
        <p className="lead mb-4">
          At Our Clinic, we are dedicated to providing high-quality healthcare
          services to our patients. With our experienced team of doctors and
          state-of-the-art facilities, we strive to ensure your well-being and
          comfort.
        </p>
        <p className="mb-4">
          Whether you need a routine check-up, specialized treatment, or expert
          advice, we are here to help. Our patient-centered approach ensures
          that you receive personalized care tailored to your unique needs.
        </p>
      </div>
    </div>
  );
}

export default Home;
