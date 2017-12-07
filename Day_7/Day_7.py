puzzlePath = "puzzleinput.txt"

def main():
    print("Recursive Circus puzzle")

    # First task
    tree = getTreeFromFile(puzzlePath)
    print("First task: " + tree.name)

    # Second task
    balancedWeight = tree.getBalancedWeight()
    print("Second task: " , int(balancedWeight))

    input("Hit enter to quit")

def getTreeFromFile(_filePath):
    """Generates a tree from the file at the specified path."""
    # Gets the node descriptions from the file.
    descriptions = _getLinesFromFile(_filePath)

    # Generate nodes, and put them in dictionary for easy lookup.
    nodes = {}
    for description in descriptions:
        components = _getComponents(description)
        node = TreeNode(components[0], int(components[1]))
        nodes[node.name] = node

    # Use the descriptions and dictionary to make associations between parents/children.
    _makeAssociations(descriptions,nodes)
    
    # Finally find the root of the tree.
    root = _getRoot(nodes)
    return root

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
    
    # Remove parentheses from the weight string.
    components[1] = components[1].replace("(","").replace(")","")

    # If it has children, remove commas from their name strings and trim to remove arrow
    if len(components) > 2:
        for i in range(3,len(components)):
            components[i-1] = components[i].replace(",","")
        components = components[:-1]
    return components

def _makeAssociations(_descriptions,nodes):
    """Based on a list of node descriptions and a dictionary of nodes,
        associates parent and child relationships between the nodes according
        to the descriptions."""
    for description in _descriptions:
        components = _getComponents(description)
        # Lookup node in dictionary.
        name = components[0]
        node = nodes[name]

        # If it has children, associate them.
        if len(components) > 2:
            for i in range (2,len(components)):
                node.addChild(nodes[components[i]])
                nodes[components[i]].setParent(node)

def _getRoot(_nodes):
    """Based on a dictionary of nodes, finds the root."""
    for name,node in _nodes.items():
        if node.parent == None:
            return node
    return None

class TreeNode:
    def __init__(self, _name,_weight):
        self.name = _name
        self.weight = _weight
        self.parent = None
        self.children = []

    def setParent(self,_parent):
        self.parent = _parent

    def addChild(self,_child):
        self.children.append(_child)

    def getBalancedWeight(self):
        """Given a tree with a single imbalanced element in it, return the weight
        the imbalanced element should have in order for the tree to be balanced."""
        if self.areChildrenBalanced():
            return self.weight-self.parent.getImbalance()
        else:
            imbalancedChild = self.getImbalancedChild()
            return imbalancedChild.getBalancedWeight()

    def areChildrenBalanced(self):
        """Checks if all the children of this node have the same branch weight. Raises
            an exception if the node has more than two different child weights."""
        weights = self.getChildWeightMap()

        # If there is only one weight mapped, all branches have the same weight.
        if len(weights) == 1:
            return True
        elif len(weights) > 2:
            raise Exception("Invalid node: More than two different weights in children!")
        else:
            return False

    def getBranchWeight(self):
        """Recursively gets the total weight of the node and all its children."""
        totalWeight = self.weight
        for child in self.children:
            totalWeight = totalWeight + child.getBranchWeight()
        return totalWeight
    
    def getChildWeightMap(self):
        """Gets a dictionary mapping the frequency of each child weight."""
        weights = {}
        for child in self.children:
            childWeight = child.getBranchWeight()
            # Add to current value if exists, otherwise initialise.
            if childWeight in weights:
                weights[childWeight] += 1
            else:
                weights[childWeight] = 1
        return weights

    def getImbalancedChild(self):
        """Returns the first child of this node whose weight is unique among this
            node's children. If none are unique it returns None. If more than two
            different weights exist in children, the node is invalid and an exception
            is raised."""
        weights = self.getChildWeightMap()
        if len(weights) > 2:
            raise Exception("Invalid node: More than two different weights in children!")

        wrongweight = 0
        for weight,frequency in weights.items():
            if frequency == 1:
                wrongweight = weight
                break;

        for child in self.children:
            if child.getBranchWeight() == wrongweight:
                return child
        return None

    def getImbalance(self):
        """Gets the amount by which the deviant child's weight deviates from the norm.
            If more than two different weights exist in children, the node is invalid
            and an exception is raised."""
        weights = self.getChildWeightMap()

        if len(weights) > 2:
            raise Exception("Invalid node: More than two different weights in children!")
        
        totalweight = 0
        # Gets the total weight of all children.
        for weight, frequency in weights.items():
            totalweight += weight*frequency

        # Looks for the unique weight, and compares it to the others' weight.
        for weight,frequency in weights.items():
            if frequency == 1:
                return weight - (totalweight - weight)/(len(self.children)-1)
        return None

if __name__ == "__main__":
    main()
