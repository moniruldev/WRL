
//Version: 2.0.3
//Date: April 30, 2014


//classes used for tab
   
    //tab class
    function xtab(tabno,tabid,linktype,menuid, name, url,param)
    {
        this.tabno=tabno;
        this.tabid=tabid;
        this.linktype=linktype;     //1=from menu,2=without menu
        this.menuid=menuid;
        this.name=name;
        this.url=url;
        this.param=param;
        
    }
    
    //menu class
    function xmenu(id,name,label,type,url,tabaction,selectaction,reload,param)
    {
        this.id=id;
        this.name=name;
        this.label=label;
        this.type=type;
        this.url=url;
        this.tabaction=tabaction;
        this.selectaction=selectaction;
        this.reload=reload;
        this.param=param;
        
        //Enums.LinkType
    }
    
    //tabmenu class for data movement
    function xtabdata(linktype,id,name,label,type,url,tabaction,selecttab, reload,param, showWait)
    {
        this.linktype=linktype;
        this.id=id;
        this.name=name;
        this.label = label;
        this.type=type;
        this.url=url;
        this.tabaction=tabaction;
        this.selecttab=selecttab;
        this.reload=reload;
        this.param = param;
        this.showWait = showWait;
        
    }