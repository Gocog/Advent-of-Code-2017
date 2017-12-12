from collections import defaultdict

puzzlePath = "puzzleinput.txt"

def main():
    print("Digital Plumber puzzle")
    nodes = buildGraphsFromFile(puzzlePath)
    
    connected = countGroup(nodes[0])
    print("First task: ",connected)

    groupCount = countGroups(nodes)
    print("Second task: ",groupCount)
    
def countGroup(_node):
    """Given a node, counts how many nodes are in its group."""
    nodes = _node.collectGroupMembers()
    return len(nodes)

def countGroups(_nodes):
    """Given a list of nodes, counts the number of groups it represents."""
    collectedNodes = defaultdict(lambda: False)
    count = 0
    for node in _nodes:
        if collectedNodes[node] == False:
            count+=1
            for member in node.collectGroupMembers():
                collectedNodes[member] = True
    return count

def buildGraphsFromFile(_filePath):
    """Builds graphs out of the contents of the file."""
    # Gets the node descriptions from the file.
    descriptions = _getLinesFromFile(_filePath)

    nodes = {}
    for description in descriptions:
        components = _getComponents(description)
        node = Node(int(components[0]))
        nodes[node.id] = node

    _makeAssociations(descriptions,nodes)

    return list(nodes.values())

def _getLinesFromFile(_filePath):
    """Gets a list of strings, one per line in the file at the specified path."""
    file = open(_filePath,"r")
    lines = []
    for line in file:
        lines.append(line)
    file.close()
    return lines

def _getComponents(description):
    """Formats a node description to a list of component strings."""
    components = description.split()
    
    # If it has assosiations, remove commas from their id strings and trim to remove arrows
    if len(components) > 2:
        for i in range(2,len(components)):
            components[i-1] = components[i].replace(",","")
        components = components[:-1]
    return components

def _makeAssociations(_descriptions,_nodes):
    """Based on a list of node descriptions and a dictionary of nodes,
        associates references between the nodes according
        to the descriptions."""
    for description in _descriptions:
        components = _getComponents(description)
        # Lookup node in dictionary.
        id = int(components[0])
        node = _nodes[id]

        # If it has references, associate them.
        if len(components) > 1:
            for i in range (1,len(components)):
                node.addReference(_nodes[int(components[i])])

class Node:
    def __init__(self, _id):
        self.id = _id
        self.references = []

    def addReference(self, _reference):
        self.references.append(_reference)

    def collectGroupMembers(self):
        """Returns a list of all nodes in this node's group."""
        visitedNodes = defaultdict(lambda: False)
        self._visit(visitedNodes)
        return list(visitedNodes.keys())

    def _visit(self, _visitedNodes):
        """Marks this node visited and visits its unvisited references."""
        _visitedNodes[self] = True
        for ref in self.references:
            if _visitedNodes[ref] == False:
                ref._visit(_visitedNodes)

if __name__ == "__main__":
    main()
