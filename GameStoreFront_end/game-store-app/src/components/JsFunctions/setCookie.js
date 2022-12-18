const setCookie = (cname, cvalue, hours) =>{
    const d = new Date();
    d.setTime(d.getTime() + (hours*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
  };

export default setCookie;