import os
import shotgun_api3.shotgun

project = None
connection = None
shotgun_instances = [
    {'name': 'a_server_at.yourcompany.com', 'script': 'your_project_plugin_script', 'key': 'your_project_plugin_script_key', 'protocol': 'https'},
    {'name': 'another_server.somewhere.com', 'script': 'plugin_script_somewhere', 'key': 'plugin_script_somewhere_key', 'protocol': 'http'},
]

def projects(user=None, fields=None):
    if fields is None:
            fields = ['code', 'id', 'name', 'sg_status']
    if user is None:
        user = os.environ['USERNAME']
    projects = []
    for inst in shotgun_instances:
        shotgun_api3.shotgun.NO_SSL_VALIDATION = True
        c = shotgun_api3.shotgun.Shotgun("%s://%s" % (inst['protocol'],inst['name']), inst['script'], inst['key'])
        conds = [['sg_status', 'is_not', 'Archive'], ['users.HumanUser.login', 'is', user]]
        inst_projects = c.find('Project', conds, fields)
        for p in inst_projects:
            p['inst'] = inst
        projects.extend(inst_projects)
    return projects

def connect(name):
    inst = [i for i in shotgun_instances if i['name'] == name]
    if len(inst) != 1:
        raise ValueError("Unknown name: %s" % name)
    shotgun_api3.shotgun.NO_SSL_VALIDATION = True
    return shotgun_api3.shotgun.Shotgun("%s://%s" % (inst[0]['protocol'],inst[0]['name']), inst[0]['script'], inst[0]['key'])
